using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public List<Option> options;
        public int correctAnswerIndex;
    }
    [System.Serializable]
    public class Option
    {
        public string optionText;
    }
    [CreateAssetMenu(fileName = "QuizData", menuName = "Quiz/QuizData", order = 1)]
    public class QuizData : ScriptableObject
    {
        public List<Question> questions;
    }
}
