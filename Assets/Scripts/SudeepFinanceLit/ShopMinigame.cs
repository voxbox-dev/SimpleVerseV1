using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class ShopMinigame : MonoBehaviour
    {
        public GameObject[] items;
        public float[] prices;

        public GameObject cashier;
        public float budget;

        private List<GameObject> pickupItems;
        private List<float> itemPrice;
        private List<GameObject> pickedUp;

        private float score;

        private string state;
        // Start is called before the first frame update
        void Start()
        {
            score = 0;
            pickupItems = new List<GameObject>(items);
            itemPrice = new List<float>(prices);
            pickedUp = new List<GameObject>();
            state = "START";
        }

        // Update is called once per frame
        void Update()
        {
            if (state.Equals("START"))
            {
                start();
            }
            else if (state.Equals("PLAY"))
            {
                
                itemPickup();
            }
            else if (state.Equals("STOP"))
            {
                finishGame();
            }
            else if (state.Equals("REPLAY"))
            {
                replay();
            }
        }

        void itemPickup()
        {
            for (int i = 0; i < pickupItems.Count; i++)
            {
                if (!pickupItems[i].activeSelf)
                {
                    score += itemPrice[i];
                    pickedUp.Add(pickupItems[i]);
                    pickupItems.Remove(pickupItems[i]);
                    itemPrice.Remove(i);
                    i--;
                }
            }
            if (!transform.Find("FinishMinigame").gameObject.activeSelf)
            {
                state = "STOP";
            }
        }

        void finishGame()
        {
            if(pickedUp.Count < 4 || score > budget)
            {
                transform.Find("TryAgain").gameObject.SetActive(true);
                state = "REPLAY";
            }
            else
            {
                transform.Find("CompleteQuiz").gameObject.SetActive(true);
                transform.Find("PicupItems").gameObject.SetActive(false);
            }
            
        }

        void replay()
        {
            if (!transform.Find("TryAgain").gameObject.activeSelf)
            {
                //transform.Find("TryAgain").gameObject.SetActive(false);
                score = 0;
                pickupItems = new List<GameObject>(items);
                foreach(GameObject obj in pickupItems)
                {
                    obj.gameObject.SetActive(true);
                }
                itemPrice = new List<float>(prices);
                pickedUp = new List<GameObject>();
                state = "PLAY";

            }
        }

        void start()
        {
            if (!transform.Find("StartMinigame").gameObject.activeSelf)
            {
                state = "PLAY";
            }
        }
    }
}
