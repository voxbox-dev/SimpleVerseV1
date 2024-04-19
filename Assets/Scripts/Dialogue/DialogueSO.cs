using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = " Dialogue/Dialogue Asset")]
    public class DialogueSO : ScriptableObject
    {
        public int RootNodeID;
        public List<DialogueNode> Nodes;


        public DialogueNode GetNodeByID(int id)
        {
            foreach (DialogueNode node in Nodes)
            {
                if (node.ID == id)
                {
                    return node;
                }
            }

            return null; // Return null if no node with the given ID is found
        }
    }
}
