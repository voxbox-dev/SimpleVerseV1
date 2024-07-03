using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
   [CreateAssetMenu(fileName = "AIModuleQuest", menuName = "ScriptableObjects/AIModuleQuest", order = 1)]
    public class AIModuleQuest : ScriptableObject
    {
        public List<GameObject> availableBillboards;
        public List<GameObject> availablePicturePrefab;
        // Add a dictionary or another suitable data structure to map images to UI GameObjects.
    }
}
