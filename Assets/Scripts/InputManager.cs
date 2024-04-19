using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{


    public class InputActionsController : ScriptableObject, IInputActionsListener
    {

        public void StartCapture()
        {
            SpatialBridge.inputService.StartCompleteCustomInputCapture(this);
        }

        public void StopCapture()
        {
            SpatialBridge.inputService.ReleaseInputCapture(this);
        }



        public void OnInputCaptureStarted(InputCaptureType type)
        {
            Debug.Log("Input Capture Started");


        }
        public void OnInputCaptureStopped(InputCaptureType type)
        {
            Debug.Log("Input Capture Stopped");


        }
    }

    public class AvatarInputController : IAvatarInputActionsListener
    {
        public void DisableMovement(bool isMoveDisabled)
        {
            Debug.Log("Disable Movement");
            SpatialBridge.inputService.StartAvatarInputCapture(isMoveDisabled, isMoveDisabled, isMoveDisabled, !isMoveDisabled, this);
        }
        public void OnAvatarActionInput(InputPhase inputPhase)
        {
            throw new System.NotImplementedException();
        }

        public void OnAvatarAutoSprintToggled(bool on)
        {
            throw new System.NotImplementedException();
        }

        public void OnAvatarInputCaptureStarted()
        {
            Debug.Log("Avatar Input Capture Started");
        }

        public void OnAvatarInputCaptureStopped()
        {
            Debug.Log("Avatar Input Capture Stopped");
        }

        public void OnAvatarJumpInput(InputPhase inputPhase)
        {
            throw new System.NotImplementedException();
        }

        public void OnAvatarMoveInput(InputPhase inputPhase, Vector2 inputMove)
        {
            throw new System.NotImplementedException();
        }

        public void OnAvatarSprintInput(InputPhase inputPhase)
        {
            throw new System.NotImplementedException();
        }

        public void OnInputCaptureStarted(InputCaptureType type)
        {
            throw new System.NotImplementedException();
        }

        public void OnInputCaptureStopped(InputCaptureType type)
        {
            throw new System.NotImplementedException();
        }
    }

}
