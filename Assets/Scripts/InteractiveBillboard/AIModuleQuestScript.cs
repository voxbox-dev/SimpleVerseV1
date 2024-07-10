using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class AIModuleQuestScript : MonoBehaviour
    {
        [SerializeField] private AIModuleQuestSO aimoduleQuestSO;
        private List<GameObject> instantiatedBillboards = new List<GameObject>();


        public void PlaceBillboards(int numberOfBillboardsToPlace)
        {
            if (BillboardManager.Instance == null)
            {
                Debug.LogError("BillboardManager instance is not available.");
                return;
            }

            if (aimoduleQuestSO.questBillboardPrefabs == null || aimoduleQuestSO.questBillboardPrefabs.Count == 0)
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
                GameObject prefabToPlace = i < aimoduleQuestSO.questBillboardPrefabs.Count ? aimoduleQuestSO.questBillboardPrefabs[i] : defaultPrefab;
                Instantiate(prefabToPlace, allPositions[i].position, allPositions[i].rotation);

                instantiatedBillboards.Add(prefabToPlace);
            }

            // If there are still positions left after placing the specified number of billboards, fill them with the default prefab
            for (int i = positionsToPlace; i < allPositions.Count; i++)
            {
                Instantiate(defaultPrefab, allPositions[i].position, allPositions[i].rotation);
            }
        }


        public void ResetBillboards()
        {
            foreach (var billboard in instantiatedBillboards)
            {
                if (billboard != null)
                {
#if UNITY_EDITOR
                    DestroyImmediate(billboard);
#else
                Destroy(billboard);
#endif
                }
            }
            instantiatedBillboards.Clear(); // Clear the list after destroying all objects

            Debug.Log("Billboards have been reset.");
        }
    }
}
