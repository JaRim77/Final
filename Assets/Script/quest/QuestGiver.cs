using UnityEngine;
using TMPro;
using System.Collections;

public class QuestGiver : Stuff, IInteractable
{
    public SO_Quest quest;

    [Header("Dialogue Settings")]
    public TMP_Text WordTextUI;
    public string[] dialogues;
    private int dialogueIndex = 0;
    private bool dialogueFinished = false;

    private bool canPress = true;

    public bool isInteractable { get => isLock; set => isLock = value; }

    public override void SetUP()
    {
        base.SetUP();

        if (WordTextUI != null)
            WordTextUI.gameObject.SetActive(false);
    }

    public void Interact(Player player)
    {
        if (!canPress) return;
        StartCoroutine(PressCooldown());

        // ⭐ ถ้ายังพูดไม่จบ → พูดทีละประโยค
        if (!dialogueFinished)
        {
            ShowDialogue(dialogues[dialogueIndex]);
            dialogueIndex++;

            // ⭐ ถ้าพูดครบแล้ว → ให้เควสต์
            if (dialogueIndex >= dialogues.Length)
            {
                dialogueFinished = true;
                Invoke(nameof(AutoGiveQuest), 2f);
            }

            return;
        }

        // ⭐ หลังให้เควสต์แล้ว — ปิดบทสนทนา
        WordTextUI.gameObject.SetActive(false);
    }

    IEnumerator PressCooldown()
    {
        canPress = false;
        yield return new WaitForSeconds(0.3f);
        canPress = true;
    }

    void ShowDialogue(string text)
    {
        if (WordTextUI != null)
        {
            WordTextUI.text = text;
            WordTextUI.gameObject.SetActive(true);
        }
    }

    void AutoGiveQuest()
    {
        WordTextUI.gameObject.SetActive(false);

        if (QuestManager.instance != null && quest != null)
        {
            QuestManager.instance.AcceptQuest(quest);
            Debug.Log("เควสต์ถูกให้โดยอัตโนมัติ: " + quest.questName);
        }

        // ⭐ ปลดล็อก Ally หลังรับเควส
        FindObjectOfType<AllyController>()?.UnlockAlly();

        // ⭐ ไม่ให้รับเควสต์ซ้ำ
        isLock = false;
    }
}
