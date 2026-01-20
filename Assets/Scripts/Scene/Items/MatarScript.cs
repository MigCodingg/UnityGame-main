using UnityEngine;

public class MatarScript : MonoBehaviour
{
    [SerializeField] Quests MatarQuest;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && MatarQuest.questProgress == Quests.QuestProgress.inProgress ) 
        {
           MatarQuest.IncrementCounter();
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
