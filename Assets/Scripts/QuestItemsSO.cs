using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

[CreateAssetMenu(fileName = "TechLab_QuestItems", menuName = "Quest/TechLab", order = 1)]
public class QuestItemsSO : ScriptableObject
{
    public uint questID;
    public List<GameObject> itemInteractionObjects;
    public List<IQuest> questList;

    public uint GetCurrentQuestID()
    {
        return SpatialBridge.questService.currentQuestID;
    }

    public List<GameObject> GetCurrentQuestItems()
    {
        var currentQuestID = GetCurrentQuestID();
        var questItems = new List<GameObject>();

        foreach (var item in itemInteractionObjects)
        {
            if (questID == currentQuestID)
            {
                questItems.Add(item);
            }
        }   

        return questItems;

    }

}