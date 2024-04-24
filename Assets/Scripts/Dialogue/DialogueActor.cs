using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;
using UnityEngine.Events;

namespace Simpleverse
{
    public class DialogueActor : MonoBehaviour
    {
        [SerializeField]
        private string speakerName;
        [SerializeField]
        private DialogueSO dialogue;

        [SerializeField]
        [Tooltip("Complete the task (id) for the current active quest. Leave at 0 if there is no task to complete")]
        private int completeTaskID = 0; // 0 means null since tasks start with id=1;


        // Serialized fields for the UnityEvents
        [SerializeField]
        private UnityEvent onInteractStartEvent;
        [SerializeField]
        private UnityEvent onInteractEndEvent;

        private DialogueManager dialogueManager;
        private VirtualCameraManager virtualCameraManager;
        private PlayerController playerController;
        private Vector3 playerTransform; // Cached player transform

        private bool hasInteractionStarted;
        private int currNodeID;


        void Start()
        {
            dialogueManager = FindAnyObjectByType<DialogueManager>();
            virtualCameraManager = FindAnyObjectByType<VirtualCameraManager>();
            playerController = FindAnyObjectByType<PlayerController>();
            hasInteractionStarted = false;
        }
        void Update()
        {
            transform.LookAt(SpatialBridge.actorService.localActor.avatar.position);
        }
        public void OnInteract()
        {
            // Invoke the event when OnInteract happens
            onInteractStartEvent?.Invoke();

            if (hasInteractionStarted == false)
            {
                // On first interaction...
                playerController.DisablePlayerMove(true); // disable movement
                virtualCameraManager.ActivateFirstPersonPOV();
                dialogueManager.SetDialoguePosition(transform.position);
                currNodeID = dialogue.RootNodeID;
                hasInteractionStarted = true;
            }

            SpeakTo(currNodeID);
            currNodeID++;

        }
        void OnEndInteract()
        {
            // Invoke the event when OnInteractEnd happens
            onInteractEndEvent?.Invoke();

            dialogueManager?.EndDialogue();
            hasInteractionStarted = false;
            playerController?.DisablePlayerMove(false); // enable movement
            virtualCameraManager?.DeactivateFirstPersonPOV();
            if (completeTaskID > 0)
            {
                var currentTask = SpatialBridge.questService.currentQuest?.GetTaskByID((uint)completeTaskID);
                if (currentTask != null)
                {
                    Debug.Log("CURR TASK" + currentTask.id);
                    Debug.Log("CURR TASK" + currentTask.name);
                    currentTask.Complete();
                }
                else
                {
                    Debug.Log("Task with ID " + completeTaskID + " not found.");
                }
            }
            else
            {
                Debug.Log("COMPLETE TASK ID is not set: " + completeTaskID);
            }
        }

        void SpeakTo(int currNodeID)
        {
            var node = dialogue?.GetNodeByID(currNodeID);
            if (node == null)
            {
                OnEndInteract();
            }
            else
            {
                dialogueManager?.StartDialogue(dialogue, speakerName, currNodeID);
            }
        }
        void OnDestroy()
        {

        }

    }
}
