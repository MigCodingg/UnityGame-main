using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField] Quests TreeQuest;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && TreeQuest.questProgress == Quests.QuestProgress.inProgress ) 
        {
            TreeQuest.IncrementCounter();
           Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
