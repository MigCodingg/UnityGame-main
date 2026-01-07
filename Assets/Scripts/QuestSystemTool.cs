using UnityEngine;
using System.Collections.Generic;

public class QuestSystemTool : MonoBehaviour
{
    private Quests[] allQuests;
    private Dictionary<Quests, int> previousCounts = new Dictionary<Quests, int>();


    private void Start()
    {
        if (QuestManager.Instance == null) return;
        if (QuestUI.Instance == null) return;

        // Manually add the Cegarro quest to activeQuests
        QuestManager.Instance.activeQuests.Add(Resources.Load<Quests>("Quests/Cegarro"));
        QuestUI.Instance.RefreshUI();
        Debug.Log("Manually added Cegarro quest");
    }



    private void Awake()
    {
        
        allQuests = Resources.LoadAll<Quests>("Quests");

        if (allQuests.Length == 0)
            Debug.LogWarning("No quests found in Resources/Quests!");

        if (QuestManager.Instance == null)
            Debug.LogError("QuestManager not found in scene!");

        if (QuestUI.Instance == null)
            Debug.LogError("QuestUI not found in scene!");
    }

    private void Update()
    {
        if (QuestManager.Instance == null || QuestUI.Instance == null)
            return;

        foreach (Quests quest in allQuests)
        {
            
            if (quest.questProgress != Quests.QuestProgress.NoStarted &&
                !QuestManager.Instance.activeQuests.Contains(quest))
            {
                QuestManager.Instance.activeQuests.Add(quest);
                Debug.Log("Quest added: " + quest.questName);
            }

            
            if (!previousCounts.ContainsKey(quest))
                previousCounts[quest] = quest.CurrentCount;

            if (previousCounts[quest] != quest.CurrentCount)
            {
                previousCounts[quest] = quest.CurrentCount;
                QuestUI.Instance.RefreshUI();
            }
        }

        
        QuestUI.Instance.RefreshUI();
    }
}
