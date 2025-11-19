using UnityEngine;

public class MonsterGO : Character
{
    public string monsterID;

    public override void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            // แจ้งเควสต์
            if (QuestManager.instance != null)
            {
                QuestManager.instance.UpdateQuestProgress(ObjectiveType.Kill, monsterID);
                GameManager.Instance.AddScore(10);
            }
            Destroy(gameObject);
        }
    }
}
