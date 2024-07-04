using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class AIModuleQuestScript : MonoBehaviour
    {
        [SerializeField] private AIModuleQuestSO aimoduleQuestSO;

        private List<Transform> updatedBillboardPositions = new List<Transform>();

        public void UpdateBillboards(int numberOfBillboardsToUpdate)
        {
            List<Transform> billboardPositions = BillboardManager.Instance.billboardPositions;
            // Randomly select and replace X billboards with X billboards from the quest
            for (int i = 0; i < numberOfBillboardsToUpdate; i++)
            {
                int positionIndex = Random.Range(0, billboardPositions.Count);
                int questBillboardIndex = Random.Range(0, aimoduleQuestSO.questBillboardPrefabs.Count);

                Transform positionToReplace = billboardPositions[positionIndex];
                GameObject questBillboardPrefab = aimoduleQuestSO.questBillboardPrefabs[questBillboardIndex];

                // Replace the billboard. Holds a reference to the new billboard
                GameObject newBillboard = Instantiate(questBillboardPrefab, positionToReplace.position, positionToReplace.rotation, positionToReplace);
                positionToReplace.gameObject.SetActive(false);

                // Keep track of updated billboard positions
                updatedBillboardPositions.Add(positionToReplace);
            }
        }

        public void ResetBillboards()
        {
            // Revert the changed billboards with defaultBillboardPrefab
            foreach (Transform updatedPosition in updatedBillboardPositions)
            {
                updatedPosition.gameObject.SetActive(true);
                // Instantiate the default prefab at the position
                Instantiate(BillboardManager.Instance.defaultBillboardPrefab, updatedPosition.position, updatedPosition.rotation, updatedPosition);

                // Destroy the quest billboard
                foreach (Transform child in updatedPosition)
                {
                    Destroy(child.gameObject);
                }
            }

            // Clear the list after resetting
            updatedBillboardPositions.Clear();
        }
    }
}
