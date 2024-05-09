using UnityEngine;
using UnityEngine.Events;
using TMPro;
using SpatialSys.UnitySDK;

namespace Simpleverse
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private UnityEvent OnEndTimer;
        [SerializeField] private GameObject TimerModel;
        [SerializeField] private TMP_Text TimerTextObject;

        [SerializeField] private Vector3 TimerPositionOffset;

        private bool timerEnded = false;
        private bool timerStarted = false;


        private float countdown;

        void Start()
        {
            // Initialize the countdown to the duration
            countdown = duration;
            // hide the timer model
            HideTimer();
            OnEndTimer.AddListener(OnEndTimerEvent);
        }

        // Destroy listeners when the object is destroyed or disabled
        void OnDestroy()
        {
            OnEndTimer.RemoveAllListeners();
        }

        void OnDisable()
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
                }
            }

        }


        private void SetPosition()
        {
            // Put timer in front of the player
            TimerModel.transform.position = SpatialBridge.actorService.localActor.avatar.position + TimerPositionOffset;
        }

        public void StartTimer()
        {
            // If the timer has already started, do nothing
            if (timerStarted)
            {
                return;
            }

            // Enable the timer model and start the countdown
            timerEnded = false;
            timerStarted = true; // Set the flag to true
            ShowTimer();
            countdown = duration;
        }

        private void OnEndTimerEvent()
        {
            countdown = 0;
            // Disable the timer model and stop the countdown
            HideTimer();

            timerStarted = false; // Reset the flag
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
