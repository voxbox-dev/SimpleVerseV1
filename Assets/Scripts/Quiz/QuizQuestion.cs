using TMPro;
using UnityEngine;

namespace Simpleverse
{
    public class QuizQuestion : MonoBehaviour
    {
        public TMP_Text questionText;
        public void SetQuestion(string text)
        {
            questionText.text = text;
        }
    }

}
