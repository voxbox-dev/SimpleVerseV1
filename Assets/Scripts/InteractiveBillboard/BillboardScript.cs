using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

namespace Simpleverse
{
    public class BillboardScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject virtualCamera, triggerView, triggerExit, billboardUI, audioObj;

        private PlayerController playerController;


        // Start is called before the first frame update
        void Start()
        {
            playerController = FindAnyObjectByType<PlayerController>();
        }

        // Interact with the billboard
        public void OnInteract()
        {
            Debug.Log("Interacting with billboard");
            // disable player movement
            playerController.DisablePlayerMove(true);
            // enable virtual camera
            virtualCamera.SetActive(true);
            // show exit button
            triggerExit.SetActive(true);
            // initially hide the billboard UI
            billboardUI.SetActive(false);
            // hide billboard view button
            triggerView.SetActive(false);
        }

        public void OnEndInteract()
        {
            Debug.Log("End interaction with billboard");
            // enable player movement
            playerController.DisablePlayerMove(false);
            // disable virtual camera
            virtualCamera.SetActive(false);
            // hide exit button
            triggerExit.SetActive(false);
            // show the billboard UI
            billboardUI.SetActive(true);
            // hide billboard view button - to prevent double task progress
            triggerView.SetActive(false);
            // if audioClip play audio clip, else do nothing
            if (audioObj != null)
            {
                audioObj.GetComponent<AudioSource>().Play();
            }
        }
    }
}
