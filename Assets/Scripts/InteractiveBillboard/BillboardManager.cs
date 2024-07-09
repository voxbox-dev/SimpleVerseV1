using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class BillboardManager : MonoBehaviour
    {
        public static BillboardManager Instance { get; private set; }
        public GameObject defaultBillboardPrefab;
        public List<Transform> billboardPositions; // Assign in-scene billboards here in the inspector

        private void Awake()
        {
            Debug.Log("Billboard Manager Awake called");
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                // Debug.Log("Destroying duplicate instance of BillboardManager");
            }
            else
            {
                Instance = this;
                // Debug.Log("Setting instance of BillboardManager");
                // InitializeBillboards(billboardPositions);
            }
        }

        // public void InitializeBillboards(List<GameObject> billboards, GameObject billboardPrefab)
        // {
        //     foreach (GameObject billboard in billboards)
        //     {
        //         Debug.Log("Initializing Billboard...");
        //         Instantiate(billboardPrefab, billboard.transform.position, billboard.transform.rotation);
        //     }
        // }
       
    }
}
