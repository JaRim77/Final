using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    public Character monster;      // มอนสเตอร์ตัวนี้
    public Slider slider;          // Slider เลือด
    private Transform cam;         // กล้อง

    void Start()
    {
        cam = Camera.main.transform;

        slider.maxValue = monster.maxHealth;
        slider.value = monster.health;
    }

    void Update()
    {
        if (monster == null)
        {
            Destroy(gameObject);   // ลบ HP bar ถ้ามอนตาย
            return;
        }

        slider.value = monster.health;

        // หันเข้าหากล้อง (Billboard)
        transform.LookAt(transform.position + cam.forward);
    }
}
