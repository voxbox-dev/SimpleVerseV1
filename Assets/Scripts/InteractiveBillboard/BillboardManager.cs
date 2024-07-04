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
            Debug.Log("Awake called");
            if (Instance != null && Instance != this)
            {
                Debug.Log("Destroying duplicate instance of VehicleManager");
                Destroy(this);
            }
            else
            {
                Debug.Log("Setting instance of VehicleManager");
                Instance = this;
                InitializeBillboards();
            }
        }

        private void InitializeBillboards()
        {
            foreach (Transform position in billboardPositions)
            {
                Instantiate(defaultBillboardPrefab, position.position, position.rotation, position);
            }
        }
    }
}
