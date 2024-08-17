using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class ShopMinigame : MonoBehaviour
    {
        public GameObject[] items;
        public float[] prices;

        public float budget;

        private List<GameObject> pickupItems;
        private List<float> itemPrice;

        private float score;
        // Start is called before the first frame update
        void Start()
        {
            score = 0;
            pickupItems = new List<GameObject>(items);
            itemPrice = new List<float>(prices);
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.gameObject.activeSelf)
            {
                itemPickup();
            }
        }

        void itemPickup()
        {
            for (int i = 0; i < pickupItems.Count; i++)
            {
                if (!pickupItems[i].activeSelf)
                {
                    score += itemPrice[i];
                    pickupItems.Remove(pickupItems[i]);
                    itemPrice.Remove(i);
                    i--;
                }
            }
        }
    }
}
