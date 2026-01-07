using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quests> activeQuests = new List<Quests>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AcceptQuest(Quests quest)
    {
        Debug.Log("Quest accepted: " + quest.questName); 

        if (!activeQuests.Contains(quest))
        {
            quest.StartQuest();
            activeQuests.Add(quest);
            QuestUI.Instance.RefreshUI();
        }
    }


    public void UpdateQuestProgress(Quests quest)
    {
        quest.IncrementCounter();
        QuestUI.Instance.RefreshUI();
    }
}
