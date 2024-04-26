using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{
    [RequireComponent(typeof(Quest))]
    public class Quest : MonoBehaviour
    {
        // This script will be attached to Quest object with a quest component. It will instantiate a marker prefab when script starts, and will activate/show and set the position of marker equal to the position of the task marker object. When the task is completed, the marker will be hidden/deactivated.
        // The marker will be used to show the player where to go to complete the task.

        [SerializeField]
        private GameObject markerPrefab;
        private GameObject marker;

        void Start()
        {
            // Instantiate the marker prefab
            marker = Instantiate(markerPrefab);
            marker.SetActive(false);
        }


    }
}
