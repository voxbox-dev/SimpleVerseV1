using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{
    public class PlayerController : MonoBehaviour, IAvatarInputActionsListener
    {
        // VARIABLES
        private bool isMoveDisabled = false;

        // METHODS
        public void DisablePlayerMove(bool disableMovement)
        {
            isMoveDisabled = disableMovement;


            if (isMoveDisabled)
            {
                // Capture and ignore input controls
                SpatialBridge.inputService.StartAvatarInputCapture(isMoveDisabled, isMoveDisabled, isMoveDisabled, false, this);
                OnInputCaptureStarted(InputCaptureType.Avatar);
                StopAvatarMovement();
            }
            else
            {
                // stop capturing input and re-enable control overrides
                SpatialBridge.inputService.ReleaseInputCapture(this);
                OnInputCaptureStopped(InputCaptureType.Avatar);
            }
        }
        private void StopAvatarMovement()
        {
            // stop avatar movement in case player holds down movement key while interacting
            SpatialBridge.actorService.localActor.avatar.Move(Vector3.zero);
        }

        public void RotateAvatarTowards(GameObject obj)
        {

            // set rotation to face object 
            Vector3 myAvatarPosition = SpatialBridge.actorService.localActor.avatar.position;
            Vector3 lookPos = obj.transform.position - myAvatarPosition;
            lookPos.y = 1;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            SpatialBridge.actorService.localActor.avatar.SetPositionRotation(myAvatarPosition, rotation);
        }

        // INTERFACE METHODS
        public void OnAvatarActionInput(InputPhase inputPhase)
        {

        }

        public void OnAvatarAutoSprintToggled(bool on)
        {

        }

        public void OnAvatarJumpInput(InputPhase inputPhase)
        {

        }

        public void OnAvatarMoveInput(InputPhase inputPhase, Vector2 inputMove)
        {

        }

        public void OnAvatarSprintInput(InputPhase inputPhase)
        {

        }

        public void OnInputCaptureStarted(InputCaptureType type)
        {


        }

        public void OnInputCaptureStopped(InputCaptureType type)
        {

        }

    }
}
