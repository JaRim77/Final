using UnityEngine;

public class ShopNPC : Stuff, IInteractable
{
    public GameObject shopUI;
    public bool isInteractable { get => isLock; set => isLock = value; }

    public override void SetUP()
    {
        base.SetUP();
        isLock = true;
        shopUI.SetActive(false);
    }

    public void Interact(Player player)
    {
        if (isLock)
        {
            shopUI.SetActive(true);
            MouseManager.Instance.UnlockMouse();   // ⭐ ปลดเมาส์
            Debug.Log("🛒 เปิดร้านค้า");
        }
    }
}
