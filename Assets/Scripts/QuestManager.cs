using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
    public class QuestManager : MonoBehaviour
    {
        private IQuest currentQuest;
        void Start()
        {
            // when quest status changes
            SpatialBridge.questService.onCurrentQuestChanged += OnCurrentQuestChanged;
        }

        // Remove events on Destroy or when no longer needed

        void OnDestroy()
        {
            SpatialBridge.questService.onCurrentQuestChanged -= OnCurrentQuestChanged;
        }
        //         public void GetCurrentQuest()
        //         {
        //             IQuest currQuest = SpatialBridge.questService.currentQuest
        //             Debug.Log("Current Quest: " + currQuest.name);
        // 
        //             // Add Event Listener to each task
        //             foreach (IQuestTask task in currQuest.tasks)
        //             {
        //                 task.onStarted += OnTaskStarted;
        //                 task.onCompleted += OnTaskCompleted;
        //                 task.onPreviouslyCompleted += OnTaskPreviouslyCompleted;
        //             }
        //         }

        public void OnCurrentQuestChanged(IQuest args)
        {
            // Add checks if the quest is null
            if (args == null)
            {
                Debug.Log("Current Quest Changed: No Quest Active");
                return;
            }
            currentQuest = args;
            Debug.Log("Current Quest Changed: Name" + args.name);
            Debug.Log("Current Quest Changed: ID" + args.id);
            Debug.Log("Current Quest Changed: Status" + args.status);
        }


    }
}