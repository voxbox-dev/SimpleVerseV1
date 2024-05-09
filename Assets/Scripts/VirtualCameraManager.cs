using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{

    public class VirtualCameraManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject npcVcam;

        [SerializeField]
        private Vector3 npcVcamPosition, npcVcamRotation;

        public void EnableNPCCam(Transform npcTransform)
        {
            // enable npc cam
            npcVcam.SetActive(true);

            // calculate the forward facing direction of npcTransform
            Vector3 npcForward = npcTransform.forward;

            // calculate the position of npcVcam away from npc
            Vector3 vcamPosition = npcTransform.position + npcForward * npcVcamPosition.magnitude;

            // adjust the height of vcamPosition
            vcamPosition.y = npcTransform.position.y + npcVcamPosition.y;

            // set the position of npcVcam
            npcVcam.transform.position = vcamPosition;

            // rotate npcVcam to face the npc
            npcVcam.transform.LookAt(npcTransform);

            // lock rotation on x and z axis
            Vector3 rotation = npcVcam.transform.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;
            npcVcam.transform.eulerAngles = rotation;
        }
        public void DisableNPCCam()
        {
            // disable npc cam
            npcVcam.SetActive(false);
        }

        public void ActivateFirstPersonPOV()
        {
            SpatialBridge.cameraService.forceFirstPerson = true;
        }
        public void DeactivateFirstPersonPOV()
        {
            SpatialBridge.cameraService.forceFirstPerson = false;
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


