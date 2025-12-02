using UnityEngine;
using TMPro;
using System.Collections;

public class QuestGiver : Stuff, IInteractable
{
    public SO_Quest quest;

    [Header("UI Dialogue")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogues;

    private int dialogueIndex = 0;
    private bool isTalking = false;
    private bool isTyping = false;

    private bool canPress = true;

    public float typeSpeed = 0.03f;

    public bool isInteractable { get => isLock; set => isLock = value; }

    public override void SetUP()
    {
        base.SetUP();
        if (dialoguePanel) dialoguePanel.SetActive(false);
    }

    public void Interact(Player player)
    {
        if (!isInteractable) return;   // ถ้าคุยจบแล้ว ห้ามคุยซ้ำ
        if (!canPress) return;
        StartCoroutine(PressCooldown());

        // ยังไม่เริ่มคุย => เริ่มคุย
        if (!isTalking)
        {
            StartDialogue();
            return;
        }

        // ถ้ายังพิมพ์ไม่เสร็จ => ห้ามกด (ไม่ skip)
        if (isTyping)
        {
            return;
        }

        // พิมพ์เสร็จแล้ว => ไปข้อความถัดไป
        NextDialogue();
    }

    void StartDialogue()
    {
        isTalking = true;
        dialogueIndex = 0;

        dialoguePanel.SetActive(true);
        StartCoroutine(TypeSentence(dialogues[dialogueIndex]));
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex >= dialogues.Length)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(TypeSentence(dialogues[dialogueIndex]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        // พิมพ์เสร็จแล้วค่อยกดได้
        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isTalking = false;
        isTyping = false;

        // ห้ามคุยซ้ำ
        isInteractable = false;

        // ให้เควสต์
        if (QuestManager.instance != null && quest != null)
            QuestManager.instance.AcceptQuest(quest);

        // ปลดล็อกเพื่อนถ้ามี
        FindObjectOfType<AllyController>()?.UnlockAlly();
    }

    IEnumerator PressCooldown()
    {
        canPress = false;
        yield return new WaitForSeconds(0.25f);
        canPress = true;
    }
}
