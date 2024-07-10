using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
    public class AIModuleQuestScript : MonoBehaviour
    {
        [SerializeField] private AIModuleQuestSO aiModuleQuestSO;
        [SerializeField] private ulong questRewardAmount = 50;
        private List<GameObject> instantiatedBillboards = new List<GameObject>();

       
        public void AwardQuestCurrency()
        {
            if (questRewardAmount <= 0)
            {
                Debug.LogError("Quest reward amount is not set.");
                return;
            }
            bool awardSucceeded = SpatialBridge.inventoryService.AwardWorldCurrency(questRewardAmount).succeeded;
            if (awardSucceeded)
            {
                Debug.Log("Player has been awarded " + questRewardAmount + " currency.");
            }
            else
            {
                Debug.LogError("Failed to award player currency.");
            }
        }


        public void PlaceBillboards(int numberOfBillboardsToPlace)
        {
            if (BillboardManager.Instance == null)
            {
                Debug.LogError("BillboardManager instance is not available.");
                return;
            }

            if (aiModuleQuestSO.questBillboardPrefabs == null || aiModuleQuestSO.questBillboardPrefabs.Count == 0)
            {
                Debug.LogError("No quest billboard prefabs available.");
                return;
            }

            List<Transform> allPositions = BillboardManager.Instance.billboardPositions;
            if (allPositions == null || allPositions.Count == 0)
            {
                Debug.LogError("No billboard positions available.");
                return;
            }

            GameObject defaultPrefab = BillboardManager.Instance.DefaultBillboardPrefab;
            if (defaultPrefab == null)
            {
                Debug.LogError("Default billboard prefab is not set in BillboardManager.");
                return;
            }


            if (allPositions == null || allPositions.Count == 0)
            {
                Debug.LogError("No positions available for placing billboards.");
                return;
            }

            // int positionsToPlace = Mathf.Min(numberOfBillboardsToPlace, allPositions.Count, aimoduleQuestSO.questBillboardPrefabs.Count + 1); // +1 for the default prefab

            int positionsToPlace = Mathf.Min(numberOfBillboardsToPlace, allPositions.Count);

            // Shuffle the list of positions
            for (int i = 0; i < allPositions.Count; i++)
            {
                Transform temp = allPositions[i];
                int randomIndex = Random.Range(i, allPositions.Count);
                allPositions[i] = allPositions[randomIndex];
                allPositions[randomIndex] = temp;
            }

            for (int i = 0; i < positionsToPlace; i++)
            {
                GameObject prefabToPlace = i < aiModuleQuestSO.questBillboardPrefabs.Count ? aiModuleQuestSO.questBillboardPrefabs[i] : defaultPrefab;
                GameObject instantiatedPrefab = Instantiate(prefabToPlace, allPositions[i].position, allPositions[i].rotation);

                instantiatedBillboards.Add(instantiatedPrefab);
            }

            // If there are still positions left after placing the specified number of billboards, fill them with the default prefab
            for (int i = positionsToPlace; i < allPositions.Count; i++)
            {
                GameObject prefabToPlace = defaultPrefab;
                GameObject instantiatedPrefab = Instantiate(prefabToPlace, allPositions[i].position, allPositions[i].rotation);

                instantiatedBillboards.Add(instantiatedPrefab);
            }
        }


        public void ResetBillboards()
        {
            foreach (var billboard in instantiatedBillboards)
            {
                if (billboard != null)
                {
                    Destroy(billboard);
                }
            }
            instantiatedBillboards.Clear(); // Clear the list after destroying all objects

            Debug.Log("Billboards have been reset.");
        }
    }
}
