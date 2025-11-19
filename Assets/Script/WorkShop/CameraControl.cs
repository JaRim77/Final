using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;           // Player
    public float mouseSensitivity = 200f;
    public Vector3 offset = new Vector3(0f, 2f, -4f);

    private float yaw = 0f;            // หมุนซ้ายขวา
    private float pitch = 0f;          // หมุนขึ้นลง

    public float minPitch = -20f;      // จำกัดกล้องก้ม
    public float maxPitch = 45f;       // จำกัดกล้อง ng
    private bool questUIOpen = false;

    private void Start()
    {
        MouseManager.Instance.LockMouse();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questUIOpen = !questUIOpen;  // toggle true/false

            if (questUIOpen)
                MouseManager.Instance.UnlockMouse();
            else
                MouseManager.Instance.LockMouse();

        }
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    private void LateUpdate()
    {
        if (target == null) return;

        // รับค่าจากเมาส์
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // หมุนซ้ายขวา
        yaw += mouseX;

        // หมุนขึ้นลง (แบบไม่ให้กล้องตีลังกา)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // สร้าง rotation ของกล้อง
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // คำนวณตำแหน่งกล้อง
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = desiredPosition;

        // มองไปที่ Player
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
