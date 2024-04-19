using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{

    public class VirtualCameraManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject virtualCamera;

        [SerializeField]
        private Vector3 virtualCameraPosition;

        public void DisableCamera()
        {
            virtualCamera.SetActive(false);
        }

        void SetCameraPosition(Vector3 position)
        {
            virtualCamera.transform.position = position;
        }
        void SetCameraRotation(Quaternion rotation)
        {
            virtualCamera.transform.rotation = rotation;
        }
        public void EnableCamera()
        {

            virtualCamera.SetActive(true);
        }

        public void ActivateFirstPersonPOV()
        {
            SpatialBridge.cameraService.forceFirstPerson = true;
        }
        public void DeactivateFirstPersonPOV()
        {
            SpatialBridge.cameraService.forceFirstPerson = false;
        }

        public void FocusOnNPC(GameObject targetObj, float interactionDistance)
        {
            if (targetObj != null)
            {
                if (!virtualCamera.activeSelf)
                {
                    EnableCamera();
                }
                // Calculate the position in front of the NPC
                Transform targetsTransform = targetObj.transform;
                Vector3 targetPosition = targetsTransform.position + targetsTransform.forward;

                // Make the player face the NPC
                Vector3 lookPos = targetPosition - SpatialBridge.actorService.localActor.avatar.position;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                SpatialBridge.actorService.localActor.avatar.rotation = rotation;

                // Position the camera slightly above and behind the NPC
                SetCameraPosition(targetsTransform.position + virtualCameraPosition);
                SetCameraRotation(rotation);
                // Make the camera always look at the NPC
                // virtualCamera.transform.LookAt(targetsTransform);
            }
            else
            {
                Debug.Log("NPCVirtualCameraObj or NPC is null");
            }
        }
        public void ActivateThirdPerson()
        {
            SpatialBridge.cameraService.zoomDistance = 1;
            SpatialBridge.cameraService.minZoomDistance = 1;
            SpatialBridge.cameraService.maxZoomDistance = 3;
            SpatialBridge.cameraService.thirdPersonFov = 60;
            SpatialBridge.cameraService.thirdPersonOffset = new Vector3(1f, 0, 0);
        }
        public void DeactivateThirdPerson()
        {
            SpatialBridge.cameraService.zoomDistance = 6;
            SpatialBridge.cameraService.minZoomDistance = 0;
            SpatialBridge.cameraService.maxZoomDistance = 10;
            SpatialBridge.cameraService.thirdPersonFov = 70;
            SpatialBridge.cameraService.thirdPersonOffset = Vector3.zero;
        }

    }

}


