using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
   [CreateAssetMenu(fileName = "BillboardsSO", menuName = "Billboards/BillboardsSO")]
    public class BillboardsSO : ScriptableObject
    {
        public List<GameObject> questBillboardPrefabs;
        public GameObject defaultBillboardPrefab;
        
    }
}
