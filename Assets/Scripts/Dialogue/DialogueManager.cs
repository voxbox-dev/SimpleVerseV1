using SpatialSys.UnitySDK;
using TMPro;
using UnityEngine;


namespace Simpleverse
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        public GameObject dialogueBoxParent;
        public TMP_Text speaker, message;

        [SerializeField]
        private Vector3 DialogueBoxPosition;

        public void StartDialogue(DialogueSO dialogue, string name, int currNodeID)
        {
            ShowDialogue();

            DialogueNode node = dialogue.GetNodeByID(currNodeID);
            speaker.text = name;
            message.text = node.dialogueText;
        }

        public void EndDialogue()
        {
            HideDialogue();
        }

        public void HideDialogue()
        {
            dialogueBoxParent.SetActive(false);
        }
        public void ShowDialogue()
        {
            dialogueBoxParent.SetActive(true);
        }
        public void SetDialoguePosition(Vector3 targetOpbjPosition)
        {
            Vector3 dialoguePosition = targetOpbjPosition + DialogueBoxPosition;
            dialogueBoxParent.transform.position = dialoguePosition;

            // Reverse the direction to face away from the player
            Vector3 oppositeDirection = dialoguePosition - SpatialBridge.actorService.localActor.avatar.position;

            // Set the target position for the dialogue box to look at
            Vector3 targetPosition = dialoguePosition + oppositeDirection;

            // Keep the dialogue box level by setting the target position's y to the dialogue box's y
            targetPosition.y = dialoguePosition.y;

            // Rotate the dialogueBox to face the player
            dialogueBoxParent.transform.LookAt(targetPosition);

        }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // Initially hide dialogue
            HideDialogue();
        }
    }
}
