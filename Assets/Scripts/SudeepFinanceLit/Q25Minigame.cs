using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
    public class Q25Minigame : MonoBehaviour
    {
        private int reward;
        // Start is called before the first frame update
        void Start()
        {
            reward = 0;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void addReward(int rewardAmount)
        {
            reward += rewardAmount;
        }
        public void awardQuestCurrency()
        {
            SpatialBridge.inventoryService.AwardWorldCurrency(((ulong)reward));
        }
    }
}
