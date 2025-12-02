using UnityEngine;

public class ShopNPC : Stuff, IInteractable
{
    public GameObject shopUI;
    public bool isInteractable { get => isLock; set => isLock = value; }

    private TPSCameraController cam;

    public override void SetUP()
    {
        base.SetUP();
        isLock = true;
        shopUI.SetActive(false);

        cam = FindObjectOfType<TPSCameraController>();
    }

    public void Interact(Player player)
    {
        if (isLock)
        {
            shopUI.SetActive(true);
            MouseManager.Instance.UnlockMouse();

            if (cam != null)
                cam.canRotate = false;       // ❌ ปิดหมุนกล้อง

            Debug.Log("🛒 เปิดร้านค้า");
        }
    }
}
