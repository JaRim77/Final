using UnityEngine;

public class QuestLogToggle : MonoBehaviour
{
    [Header("Quest Panel")]
    public GameObject questPanel;

    private bool isOpen = false;

    private void Start()
    {
        if (questPanel != null)
            questPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestUI();
        }
    }

    public void ToggleQuestUI()
    {
        if (questPanel == null) return;

        isOpen = !isOpen;
        questPanel.SetActive(isOpen);

        if (isOpen)
        {
            MouseManager.Instance.UnlockMouse();   // ⭐ ใช้ MouseManager
        }
        else
        {
            MouseManager.Instance.LockMouse();     // ⭐ ใช้ MouseManager
        }
    }
}
