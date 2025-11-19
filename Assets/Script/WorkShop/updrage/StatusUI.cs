using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public Player player;

    [Header("UI Text")]
    public TMP_Text damageText;
    public TMP_Text speedText;
    public TMP_Text maxHpText;
    public TMP_Text currentHpText;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        UpdateStatusUI();
    }

    private void Update()
    {
        UpdateStatusUI();  // Õ—ª‡¥µ∑ÿ°‡ø√¡ (‡∫“¡“°‡æ√“–·§Ë set text)
    }

    private void UpdateStatusUI()
    {
        if (player == null) return;

        damageText.text = "Damage: " + player.Damage;
        speedText.text = "Speed: " + player.movementSpeed;
        maxHpText.text = "Max HP: " + player.maxHealth;
        currentHpText.text = "HP: " + player.health;
    }
}
