using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    [Tooltip("All quests in the game to monitor")]
    public Quests[] allQuests;

    private void Start()
    {
        // Ensure QuestManager exists
        if (QuestManager.Instance == null)
        {
            Debug.LogError("QuestManager not found in scene!");
        }

        if (QuestUI.Instance == null)
        {
            Debug.LogError("QuestUI not found in scene!");
        }
    }

    private void Update()
    {
        if (QuestManager.Instance == null || QuestUI.Instance == null)
            return;

        foreach (Quests quest in QuestManager.Instance.activeQuests)
        {
            // Force UI refresh every frame for simplicity
            // Only the quests in activeQuests are displayed
            QuestUI.Instance.RefreshUI();
        }
    }
}
