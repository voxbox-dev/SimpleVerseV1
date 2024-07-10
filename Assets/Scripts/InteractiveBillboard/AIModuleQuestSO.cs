using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
   [CreateAssetMenu(fileName = "AIModuleQuestSO", menuName = "AI Module/AIModuleQuestSO")]
    public class AIModuleQuestSO : ScriptableObject
    {
        public List<GameObject> questBillboardPrefabs;
        public GameObject defaultBillboardPrefab;
    }
}
