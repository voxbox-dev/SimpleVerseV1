using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
   [CreateAssetMenu(fileName = "AIModuleQuest", menuName = "ScriptableObjects/AIModuleQuest", order = 1)]
    [System.Serializable]
    public class PictureInfo
    {
        public GameObject pictureInfoPrefab; // Spatial Point of Interest prefab
        public Sprite image;

    }

public class AIModuleQuest : ScriptableObject
{
    public List<GameObject> availableBillboards;
    public List<PictureInfo> availableImages;
    // Add a dictionary or another suitable data structure to map images to UI GameObjects.
}
}
