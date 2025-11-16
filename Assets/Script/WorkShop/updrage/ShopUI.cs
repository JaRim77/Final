using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("References")]
    public Player player;

    [Header("Upgrade Settings")]
    public UpgradeData damageUpgrade;
    public UpgradeData speedUpgrade;
    public UpgradeData healthUpgrade;

    [Header("UI Elements")]
    public TextMeshProUGUI moneyText;
    public Button damageButton;
    public Button speedButton;
    public Button healthButton;

    private void OnEnable()
    {
        UpdateMoneyUI(); // ← อัปเดตทุกครั้งที่เปิดร้าน
    }

    private void Start()
    {
        // ผูกปุ่มแค่ครั้งเดียวตอนเริ่มเกม
        damageButton.onClick.AddListener(() => TryBuyUpgrade(damageUpgrade));
        speedButton.onClick.AddListener(() => TryBuyUpgrade(speedUpgrade));
        healthButton.onClick.AddListener(() => TryBuyUpgrade(healthUpgrade));
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void UpdateMoneyUI()
    {
        moneyText.text = "Money: " + GameManager.Instance.currentScore;
    }

    public void TryBuyUpgrade(UpgradeData upgrade)
    {
        if (GameManager.Instance.SpendScore(upgrade.cost))
        {
            switch (upgrade.type)
            {
                case UpgradeType.Damage:
                    player.Damage += upgrade.increaseAmount;
                    break;

                case UpgradeType.Speed:
                    player.movementSpeed += upgrade.increaseAmount;
                    break;

                case UpgradeType.MaxHealth:
                    player.maxHealth += upgrade.increaseAmount;
                    player.health = player.maxHealth;
                    break;
            }

            UpdateMoneyUI();
            Debug.Log("อัปเกรดสำเร็จ!");
        }
        else
        {
            Debug.Log("เงินไม่พอ!");
        }
    }
}
