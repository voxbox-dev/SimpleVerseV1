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
            Debug.Log("Camera position set to: " + position);
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

        public void FocusOnNPC(GameObject targetObj)
        {
            if (targetObj != null)
            {
                if (!virtualCamera.activeSelf)
                {
                    EnableCamera();
                }
                // Calculate the position in front of the NPC
                Transform targetsTransform = targetObj.transform;

                // adjust postion and rotation of virtual camera to be in front of the targetObj
                Vector3 targetPosition = targetsTransform.position + targetsTransform.forward;
                Vector3 lookPos = targetPosition - virtualCameraPosition;
                // Make the camera always look at the NPC
                virtualCamera.transform.LookAt(targetsTransform);
                // Quaternion rotation = Quaternion.LookRotation(lookPos);

                // Position the camera slightly above and behind the NPC
                SetCameraPosition(targetPosition + virtualCameraPosition);
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


