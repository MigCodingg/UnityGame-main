using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{


    public static QuestUI Instance;

    public Transform questListParent;
    public GameObject questItemPrefab;




    private void Awake()
    {
        Instance = this;
        Debug.Log("QuestUI Awake");
    }

    public void RefreshUI()
    {
        if (questListParent == null)
        {
            Debug.LogError("Quest List Parent not assigned!");
            return;
        }

        if (questItemPrefab == null)
        {
            Debug.LogError("Quest Item Prefab not assigned!");
            return;
        }

        // Clear previous UI items
        foreach (Transform child in questListParent)
            Destroy(child.gameObject);

        foreach (Quests quest in QuestManager.Instance.activeQuests)
        {
            // Only display quests that are in progress or done
            if (quest.questProgress == Quests.QuestProgress.NoStarted)
                continue;

            GameObject item = Instantiate(questItemPrefab, questListParent);

            TMP_Text titleText = item.transform.Find("QuestTitle").GetComponent<TMP_Text>();
            TMP_Text statusText = item.transform.Find("QuestStatus").GetComponent<TMP_Text>();

            titleText.text = quest.questName;

            if (quest.questProgress == Quests.QuestProgress.inProgress)
            {
                titleText.color = Color.white;
                statusText.color = Color.white;
                statusText.text = $"{quest.CurrentCount}/{quest.ObjectiveCount}";
            }
            else if (quest.questProgress == Quests.QuestProgress.Done)
            {
                titleText.color = Color.green;
                statusText.color = Color.green;
                statusText.text = "Mission complete!";
            }
        }
    }


}
