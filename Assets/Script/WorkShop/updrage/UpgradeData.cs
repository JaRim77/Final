using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Upgrade Data", fileName = "NewUpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public int cost;
    public int increaseAmount;
    public UpgradeType type;
}

public enum UpgradeType
{
    Damage,
    Speed,
    MaxHealth
}
