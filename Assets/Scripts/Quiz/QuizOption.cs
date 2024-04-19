using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Simpleverse
{
    public class QuizOption : MonoBehaviour
    {
        [SerializeReference] private float delayBetweenOptions = 0.5f; // The delay between each option rising
        [SerializeReference] private TMP_Text optionText;
        [SerializeReference] private string triggerRiseName;
        [SerializeReference] private string triggerHideName;
        private bool isCorrect;
        private QuizManager quizManager;
        private Animator animator; // Reference to the Animator component
        private bool hasStartedRise = false;

        // PUBLIC METHODS
        public void SetOption(string text, bool isCorrect)
        {
            optionText.text = text;
            this.isCorrect = isCorrect;
        }

        public void OnSelectOption()
        {
            quizManager.OnOptionSelected(isCorrect, delayBetweenOptions);
        }

        public IEnumerator DisplayOptionCoroutine(GameObject option, int enumeratorIndex)
        {
            // Wait for the delay
            float delay = enumeratorIndex == 0 ? delayBetweenOptions : enumeratorIndex;
            yield return new WaitForSeconds(delay * delayBetweenOptions);

            // Enable the option
            option.SetActive(true);

            StartCoroutine(StartRise());
        }

        public IEnumerator HideOptionCoroutine(GameObject option, int enumeratorIndex)
        {
            StartCoroutine(StartHide());
            // Wait for the delay
            float delay = enumeratorIndex == 0 ? delayBetweenOptions + delayBetweenOptions : enumeratorIndex * delayBetweenOptions + delayBetweenOptions;
            yield return new WaitForSeconds(delay);
            // Enable the option
            option.SetActive(false);
        }

        // PRIVATE METHODS
        void Start()
        {
            quizManager = FindObjectOfType<QuizManager>();
            // Get the Animator component
            animator = GetComponent<Animator>();
        }
        void OnDisable()
        {
            // Reset the flag
            hasStartedRise = false;
        }

        IEnumerator StartRise()
        {
            // If the object is active and the Animator is initialized, start the rise animation
            if (!hasStartedRise && gameObject.activeInHierarchy && animator.isInitialized)
            {
                animator.Update(0);
                animator.SetTrigger(triggerRiseName);
                hasStartedRise = true;
            }

            yield break;
        }
        IEnumerator StartHide()
        {
            // Play the hide animation
            animator.Update(0);
            animator.SetTrigger(triggerHideName);

            yield break;
        }


    }
}
