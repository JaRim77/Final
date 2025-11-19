using UnityEngine;

public class Coin : Item
{
    public int ScoreValue = 10;
    public AudioClip SoundCoin;
    public override void OnCollect(Player player)
    {
        base.OnCollect(player);
        GameManager.Instance.AddScore(ScoreValue);
        SoundManager.instance.PlaySFX(SoundCoin);
        Destroy(gameObject);
    }
}
