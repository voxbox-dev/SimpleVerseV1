using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
    enum ModuleName
    {
        AI,
        App_Dev
    }

    public class BillboardQuestScript : MonoBehaviour
    {
        [SerializeField] private BillboardsSO billboardsSO;

        [SerializeField] private ulong questRewardAmount = 50;

        // provide tooltip
        [Tooltip("Select the module that this quest is apart of.")]
        [SerializeField] private ModuleName moduleName;
        private List<GameObject> instantiatedBillboards = new List<GameObject>();
        private List<Transform> questBillboardPositions = new List<Transform>();



        // on start, if AI is selected, set positions for AI billboards to BillboardManager.techBillboardPositions, else set positions for App_Dev billboards to BillboardManager.appDevBillboardPositions
        private void Start()
        {
            if (billboardsSO == null)
            {
                Debug.LogError("BillboardsSO is not set.");
                return;
            }
        }

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


        public void PlaceBillboards()
        {
            GameObject defaultPrefab = BillboardManager.Instance.DefaultBillboardPrefab;
            if (defaultPrefab == null)
            {
                Debug.LogError("Default billboard prefab is not set in BillboardManager.");
                return;
            }

            if (BillboardManager.Instance == null)
            {
                Debug.LogError("BillboardManager instance is not available.");
                return;
            }

            if (billboardsSO.questBillboardPrefabs == null || billboardsSO.questBillboardPrefabs.Count == 0)
            {
                Debug.LogError("No quest billboard prefabs available.");
                return;
            }

            // Set billboards based on the module name
            if (moduleName == ModuleName.AI)
            {
                questBillboardPositions = BillboardManager.Instance.aiBillboardPositions;
                //Log the count of the quest billboard positions
                Debug.Log("Success Quest billboard positions count: " + questBillboardPositions.Count);
            }
            else if (moduleName == ModuleName.App_Dev)
            {
                questBillboardPositions = BillboardManager.Instance.appDevBillboardPositions;
                //Log the count of the quest billboard positions
                Debug.Log("Success Quest billboard positions count: " + questBillboardPositions.Count);
            }
            else
            {
                Debug.LogError("Module name is not set.");
            }

            if (questBillboardPositions == null || questBillboardPositions.Count == 0)
            {
                Debug.LogError("No billboard positions available.");
                return;
            }
            else
            {
                int positionsToPlace = Mathf.Min(billboardsSO.questBillboardPrefabs.Count, questBillboardPositions.Count);

                // Shuffle the list of positions
                for (int i = 0; i < questBillboardPositions.Count; i++)
                {
                    Transform temp = questBillboardPositions[i];
                    int randomIndex = Random.Range(i, questBillboardPositions.Count);
                    questBillboardPositions[i] = questBillboardPositions[randomIndex];
                    questBillboardPositions[randomIndex] = temp;
                }

                for (int i = 0; i < positionsToPlace; i++)
                {
                    GameObject prefabToPlace = i < billboardsSO.questBillboardPrefabs.Count ? billboardsSO.questBillboardPrefabs[i] : defaultPrefab;
                    GameObject instantiatedPrefab = Instantiate(prefabToPlace, questBillboardPositions[i].position, questBillboardPositions[i].rotation);

                    instantiatedBillboards.Add(instantiatedPrefab);
                }

                // If there are still positions left after placing the specified number of billboards, fill them with the default prefab
                for (int i = positionsToPlace; i < questBillboardPositions.Count; i++)
                {
                    GameObject prefabToPlace = defaultPrefab;
                    GameObject instantiatedPrefab = Instantiate(prefabToPlace, questBillboardPositions[i].position, questBillboardPositions[i].rotation);

                    instantiatedBillboards.Add(instantiatedPrefab);
                }
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
