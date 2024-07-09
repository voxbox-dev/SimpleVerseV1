using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class AIModuleQuestScript : MonoBehaviour
    {
        [SerializeField] private AIModuleQuestSO aimoduleQuestSO;

        public void PlaceBillboards(int numberOfBillboardsToPlace)
        {
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

            int positionsToPlace = Mathf.Min(numberOfBillboardsToPlace, allPositions.Count, aimoduleQuestSO.questBillboardPrefabs.Count);

            // Shuffle the list of positions
            for (int i = 0; i < allPositions.Count; i++)
            {
                Transform temp = allPositions[i];
                int randomIndex = Random.Range(i, allPositions.Count);
                allPositions[i] = allPositions[randomIndex];
                allPositions[randomIndex] = temp;
            }

            // Create a temporary list of prefabs to track which have been placed
            List<GameObject> availablePrefabs = new List<GameObject>(aimoduleQuestSO.questBillboardPrefabs);

            for (int i = 0; i < positionsToPlace; i++)
            {
                Transform position = allPositions[i];
                int prefabIndex = Random.Range(0, availablePrefabs.Count);
                GameObject questBillboardPrefab = availablePrefabs[prefabIndex];

                Instantiate(questBillboardPrefab, position.position, position.rotation, position);

                // Remove the placed prefab from the list to avoid duplicates
                availablePrefabs.RemoveAt(prefabIndex);

                // If we've placed all unique prefabs, break out of the loop
                if (availablePrefabs.Count == 0)
                {
                    Debug.LogWarning("All unique prefabs have been placed.");
                    break;
                }
            }
        }


        public void ResetBillboards()
        {
            // Check if billboardPositions is null or empty
            if (BillboardManager.Instance.billboardPositions == null || BillboardManager.Instance.billboardPositions.Count == 0)
            {
                Debug.LogWarning("No billboard positions available to reset.");
                return;
            }

            foreach (Transform position in BillboardManager.Instance.billboardPositions)
            {
                // Use a backward loop for safe removal of children during iteration
                for (int i = position.childCount - 1; i >= 0; i--)
                {
                    Transform child = position.GetChild(i);
#if UNITY_EDITOR
                    // Use DestroyImmediate in the editor
                    DestroyImmediate(child.gameObject);
#else
            Destroy(child.gameObject);
#endif
                }
            }

            Debug.Log("Billboards have been reset.");
        }
    }
}
