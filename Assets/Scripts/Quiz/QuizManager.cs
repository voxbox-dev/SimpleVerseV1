using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Simpleverse
{
    public class QuizManager : MonoBehaviour
    {

        // Serialized Fields
        [SerializeField]
        private QuizData quizData;
        [SerializeField]
        private GameObject questionObjectPrefab;
        [SerializeField]
        private GameObject optionObjectPrefab;
        [SerializeField]
        private GameObject optionContainer;
        [SerializeField]
        private GameObject resultsPrefab;
        [SerializeField]
        private Transform resultsContainer;
        [SerializeField]
        private GameObject TriggerStart;
        [SerializeField]
        private GameObject TriggerRestart;
        [SerializeField]
        private GameObject TriggerClaim;

        [SerializeField]
        private Transform questionContainer;
        [SerializeField]
        private List<Transform> optionContainers;
        [SerializeField]
        private int pointsPerQuestion;

        [SerializeField]
        private GameObject prizePrefab;

        [SerializeField]
        private Transform prizeContainer;

        [SerializeField]
        private GameObject resultsAnimationPrefab;

        // Private Fields
        private int currentQuestionIndex = 0;
        private int score = 0;
        private int totalPossibleScore;
        // private List<GameObject> instantiatedObjects = new();
        private List<GameObject> optionObjects = new();
        private List<string> wrongQuestions = new List<string>();
        private string currQuestionText;

        // Types
        private Question currQuestion;
        private QuizQuestion questionObjectScript;
        private QuizResults resultsObjectScript;
        // Objects
        private GameObject questionObject;
        private GameObject resultsObject;
        private GameObject prizeObject;


        void Start()
        {
            totalPossibleScore = quizData.questions.Count * pointsPerQuestion;
            questionObjectScript = questionObjectPrefab.GetComponent<QuizQuestion>();
            resultsObjectScript = resultsPrefab.GetComponent<QuizResults>();
        }
        public void StartQuiz()
        {
            currQuestion = quizData.questions[currentQuestionIndex];
            currQuestionText = currQuestion.questionText;
            TriggerStart.SetActive(false);

            if (currQuestion != null)
            {
                // If the question object already exists, just update the question text
                if (questionObject != null)
                {
                    questionObjectScript = questionObject.GetComponent<QuizQuestion>();
                    questionObjectScript.SetQuestion(currQuestionText);
                }
                else
                {
                    // Otherwise, instantiate the question object
                    questionObject = Instantiate(questionObjectPrefab, questionContainer);
                    questionObjectScript = questionObject.GetComponent<QuizQuestion>();
                    questionObjectScript.SetQuestion(currQuestionText);
                }

                questionObject.SetActive(true);

                DisplayOptions(currQuestion);
            }
            else
            {
                Debug.Log("STARTQUIZ - QUESTION NULL:  " + currQuestion);
            }
        }
        public void DisplayOptions(Question question)
        {
            // If option objects already exist, just update the option texts
            if (optionObjects.Count > 0)
            {
                for (int i = 0; i < optionObjects.Count; i++)
                {
                    QuizOption optionObjectScript = optionObjects[i].GetComponent<QuizOption>();
                    optionObjectScript.SetOption(question.options[i].optionText, i == question.correctAnswerIndex);
                    StartCoroutine(optionObjectScript.DisplayOptionCoroutine(optionObjects[i], i));

                }
            }
            else
            {
                // Otherwise, instantiate the option objects
                for (int i = 0; i < question.options.Count; i++)
                {
                    GameObject optionObject = Instantiate(optionObjectPrefab, optionContainers[i]);
                    QuizOption optionObjectScript = optionObject.GetComponent<QuizOption>();
                    optionObjectScript.SetOption(question.options[i].optionText, i == question.correctAnswerIndex);
                    StartCoroutine(optionObjectScript.DisplayOptionCoroutine(optionObject, i));
                    optionObjects.Add(optionObject); ;
                }
            }
        }

        public void OnOptionSelected(bool isCorrect, float delayBetweenOptions)
        {
            StartCoroutine(OnOptionSelectedCoroutine(isCorrect, delayBetweenOptions));
        }

        private IEnumerator OnOptionSelectedCoroutine(bool isCorrect, float delayBetweenOptions)
        {
            for (int i = 0; i < optionObjects.Count; i++)
            {
                QuizOption optionObjectScript = optionObjects[i].GetComponent<QuizOption>();
                // Add a delay before starting each coroutine
                yield return new WaitForSeconds(i * delayBetweenOptions);
                StartCoroutine(optionObjectScript.HideOptionCoroutine(optionObjects[i], i));
            }

            // Wait for the last hide animation to finish
            yield return new WaitForSeconds(optionObjects.Count * delayBetweenOptions);

            if (isCorrect)
            {
                // add to score 
                score += 10;
                Debug.Log("CORRECT" + score);
            }
            else
            {
                // If the wrong option was selected, save question
                string question = currQuestion.questionText;
                wrongQuestions.Add(question);
                Debug.Log("ADDED WRONG QUESTION: " + question);
            }

            // If there are more questions, go to the next question
            if (currentQuestionIndex + 1 < quizData.questions.Count)
            {
                currentQuestionIndex++;
                StartQuiz();
            }
            else
            {
                // If there are no more questions, end the quiz
                EndQuiz();
            }

            yield break;
        }

        void EndQuiz()
        {
            DestroyInstantiatedObjs();
            DisplayResults();
            ActivateAnimations();
        }
        public void DisplayResults()
        {
            string result;
            // Calculate final score as a percentage
            int finalScore = score * 100 / totalPossibleScore;
            string scoreText = finalScore + "%";

            // Pass or Fail?
            if (finalScore == 100)
            {
                result = "Completed!";
                SetClaimActive();
                TriggerRestart.SetActive(false);
            }
            else
            {
                result = "Try Again";
                TriggerRestart.SetActive(true);
            }

            // If the results object already exists, just update the results text
            if (resultsObject != null)
            {
                resultsObjectScript = resultsObject.GetComponent<QuizResults>();
                resultsObjectScript.SetResultsText(result);
                resultsObjectScript.SetResultsScoreText(scoreText);
            }
            else
            {
                // Otherwise, instantiate the results object
                resultsObject = Instantiate(resultsPrefab, resultsContainer);
                resultsObjectScript = resultsObject.GetComponent<QuizResults>();
                resultsObjectScript.SetResultsText(result);
                resultsObjectScript.SetResultsScoreText(scoreText);
            }

            resultsObject.SetActive(true);
        }
        void SetClaimActive()
        {

            // If the prizeObject has been spawned, show the TriggerClaim object
            if (prizeObject == null)
            {
                TriggerClaim.SetActive(true);
            }
            else
            {
                TriggerClaim.SetActive(false);
            }
        }

        public void ClaimPrize()
        {
            Reset();
            SpawnPrize();
        }

        void SpawnPrize()
        {
            // If the prizeObject  already exists, just update the results text
            if (prizeObject == null)
            {
                // Otherwise, instantiate the results object
                prizeObject = Instantiate(prizePrefab, prizeContainer);
            }

            prizeObject.SetActive(true);
        }
        public void DestroyInstantiatedObjs()
        {

            // Destroy the question object if it exists
            if (questionObject != null)
            {
                Destroy(questionObject);
                questionObject = null;
            }

            // Destroy the results object if it exists
            if (resultsObject != null)
            {
                Destroy(resultsObject);
                resultsObject = null;
            }

            //  Destroy prize
            if (prizeObject != null)
            {
                Destroy(prizeObject);
                prizeObject = null;
            }


            // Destroy the option objects if they exist
            foreach (GameObject optionObject in optionObjects)
            {
                if (optionObject != null)
                {
                    Destroy(optionObject);
                }
            }
            optionObjects.Clear();
        }
        public void Reset()
        {
            currentQuestionIndex = 0;
            score = 0;
            TriggerStart.SetActive(false);
            // Hide objects
            TriggerRestart.SetActive(false);
            TriggerClaim.SetActive(false);

            DeactivateAnimations();
            DestroyInstantiatedObjs();
        }

        void ActivateAnimations()
        {
            resultsAnimationPrefab.SetActive(true);
        }
        void DeactivateAnimations()
        {
            resultsAnimationPrefab.SetActive(false);
        }
    }
}


