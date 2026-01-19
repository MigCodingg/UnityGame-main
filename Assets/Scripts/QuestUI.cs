using UnityEngine;
using UnityEngine.UI; // Needed for LayoutRebuilder
using TMPro;
using System.Collections;

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

    // Public method to refresh the UI
    public void RefreshUI()
    {
        // Start a coroutine to ensure layout is ready
        StartCoroutine(RefreshUICoroutine());
    }

    // Coroutine ensures Vertical Layout Group has initialized before adding items
    private IEnumerator RefreshUICoroutine()
    {
        // Wait one frame for Canvas/layout initialization
        yield return null;

        if (questListParent == null)
        {
            Debug.LogError("Quest List Parent not assigned!");
            yield break;
        }

        if (questItemPrefab == null)
        {
            Debug.LogError("Quest Item Prefab not assigned!");
            yield break;
        }

        // Clear previous UI items
        foreach (Transform child in questListParent)
            Destroy(child.gameObject);

        // Add all active quests
        foreach (Quests quest in QuestManager.Instance.activeQuests)
        {
            if (quest.questProgress == Quests.QuestProgress.NoStarted)
                continue;

            GameObject item = Instantiate(questItemPrefab);
            item.transform.SetParent(questListParent, false); // keeps local scale/position
            item.transform.localScale = Vector3.one;          // ensure correct scale

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
                
            }
        }

        // Force the Vertical Layout Group to update immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(questListParent.GetComponent<RectTransform>());
    }
}
