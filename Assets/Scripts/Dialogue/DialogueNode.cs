using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    [System.Serializable]
    public class DialogueNode
    {

        public int ID;
        [TextAreaAttribute(1, 3)]
        public string dialogueText;
        public List<DialogueResponse> responses;

        internal bool IsLastNode()
        {
            return responses.Count <= 0;
        }

    }
}
