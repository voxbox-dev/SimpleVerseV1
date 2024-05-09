using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Simpleverse
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private UnityEvent OnEndTimer;
        [SerializeField] private GameObject TimerModel;
        [SerializeField] private TextMeshProUGUI TimerTextObject;

        private float countdown;

        void Start()
        {
            // Initialize the countdown to the duration
            countdown = duration;
        }

        void Update()
        {
            // Decrease the countdown by the time since the last frame
            countdown -= Time.deltaTime;

            // Format the countdown as minutes and seconds and display it
            int minutes = Mathf.FloorToInt(countdown / 60F);
            int seconds = Mathf.FloorToInt(countdown - minutes * 60);
            TimerTextObject.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // If the countdown has reached 0, invoke the OnEndTimer event
            if (countdown <= 0)
            {
                countdown = 0;
                OnEndTimer.Invoke();
            }
        }

        public void StartTimer()
        {
            // Enable the timer model and start the countdown
            TimerModel.SetActive(true);
            countdown = duration;
        }

        public void OnEndTimerEvent()
        {
            // Disable the timer model and stop the countdown
            TimerModel.SetActive(false);
            countdown = 0;
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
