using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterHealthBar : MonoBehaviour
{
    public Character monster;      // มอนสเตอร์ตัวนี้
    public Slider slider;          // HP bar
    public TMP_Text hpText;        // ข้อความตัวเลข HP

    private Transform cam;         // กล้อง

    void Start()
    {
        cam = Camera.main.transform;

        slider.maxValue = monster.maxHealth;
        slider.value = monster.health;

        UpdateHPText();
    }

    void Update()
    {
        if (monster == null)
        {
            Destroy(gameObject);   // ลบ HP bar ถ้ามอนตาย
            return;
        }

        slider.value = monster.health;

        UpdateHPText();

        // Billboard → หันเข้าหากล้อง
        transform.LookAt(transform.position + cam.forward);
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"{monster.health} / {monster.maxHealth}";
        }
    }
}
