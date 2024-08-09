using UnityEngine;
using UnityEngine.Events;
using TMPro;
using SpatialSys.UnitySDK;

namespace Simpleverse
{
    public class CountdownTimer : MonoBehaviour
    {
        //sudeep was here

        [SerializeField] private float duration;
        [SerializeField] private UnityEvent OnEndTimer;
        [SerializeField] private GameObject TimerModel;
        [SerializeField] private TMP_Text TimerTextObject;

        [SerializeField] private Vector3 TimerPositionOffset;

        [SerializeField] private Position timerPosition = Position.RIGHT;

        private enum Position { BEHIND, RIGHT };
        private bool timerEnded = false;
        private bool timerStarted = false;


        private float countdown;

        void Start()
        {
            // Initialize the countdown to the duration
            countdown = duration;
            // hide the timer model
            StopTimer();
            OnEndTimer.AddListener(StopTimer);
            Debug.Log("Timer Initialized");
        }

        // Destroy listeners when the object is destroyed or disabled
        void OnDestroy()
        {
            OnEndTimer.RemoveAllListeners();
        }


        void Update()
        {
            if (!timerEnded)
            {
                SetPosition();
                countdown -= Time.deltaTime;

                // Format the countdown as minutes and seconds and display it
                int minutes = Mathf.FloorToInt(countdown / 60F);
                int seconds = Mathf.FloorToInt(countdown - minutes * 60);
                TimerTextObject.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                if (countdown <= 0)
                {
                    countdown = 0;
                    timerEnded = true;
                    OnEndTimer?.Invoke();
                    StartTimer();
                }
            }
        }

        private void SetPosition()
        {
            // Get forward facing direction of the player
            Vector3 playerPosition = SpatialBridge.actorService.localActor.avatar.position;
            Quaternion playerRotation = SpatialBridge.actorService.localActor.avatar.rotation;
            Vector3 forwardDirection = playerRotation * Vector3.forward;
            if (timerPosition == Position.BEHIND)
            {
                // Place timer behind the player by offset distance
                TimerModel.transform.position = TimerModel.transform.position - forwardDirection * TimerPositionOffset.magnitude;
            }
            // Place timer on the right side of the player by offset distance
            TimerModel.transform.position = playerPosition + playerRotation * (Vector3.right * TimerPositionOffset.magnitude);
            // adjust height to set above ground
            TimerModel.transform.position = new Vector3(TimerModel.transform.position.x, playerPosition.y + TimerPositionOffset.y, TimerModel.transform.position.z);

            TimerModel.transform.LookAt(playerPosition);

            // Keep timer flat on the ground
            TimerModel.transform.rotation = Quaternion.Euler(90, playerRotation.eulerAngles.y, playerRotation.eulerAngles.z);
        }

        public void StartTimer()
        {
            // If the timer has already started, do nothing
            if (timerStarted)
            {
                return;
            }
            Debug.Log("Starting Timer");
            // Enable the timer model and start the countdown
            timerEnded = false;
            timerStarted = true; // Set the flag to true
            ShowTimer();
            countdown = duration;
        }

        public void StopTimer()
        {
            Debug.Log("Stopping Timer");
            countdown = 0;
            // Disable the timer model and stop the countdown
            HideTimer();
            timerStarted = false; // Reset the flag
            timerEnded = true;
        }

        public void ShowTimer()
        {
            // Enable the timer model
            TimerModel.SetActive(true);
        }

        public void HideTimer()
        {
            // Disable the timer model
            TimerModel.SetActive(false);
        }
    }

}
