using UnityEngine;

public class AllyController : MonoBehaviour
{
    public Ally ally;

    // 🔒 เริ่มเกม Ally ยังใช้ไม่ได้
    public bool allyUnlocked = false;

    private AllyCommand currentCommand = AllyCommand.None;

    void Update()
    {
        if (ally == null) return;

        // ❌ ถ้ายังไม่ปลดล็อก — ห้ามทำอะไรเลย
        if (!allyUnlocked)
            return;

        // ⬇️ ปุ่มสั่ง Ally ทำงานเฉพาะตอนปลดล็อกแล้ว
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentCommand = AllyCommand.Follow;
            Debug.Log("Follow Me!");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            currentCommand = AllyCommand.Attack;
            Debug.Log("Attack!!");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentCommand = AllyCommand.CollectItem;
            Debug.Log("Search for items");
        }

        // คำสั่ง AI
        switch (currentCommand)
        {
            case AllyCommand.Follow:
                ally.FollowPlayer();
                break;

            case AllyCommand.Attack:
                ally.HandleEnemy();
                break;

            case AllyCommand.CollectItem:
                ally.TryCollectNearbyItem();
                break;

            case AllyCommand.None:
                break;
        }
    }

    // 🔓 ฟังก์ชันให้ Quest ปลดล็อก Ally
    public void UnlockAlly()
    {
        allyUnlocked = true;
        Debug.Log("Ally unlocked and ready to help!");
    }
}
