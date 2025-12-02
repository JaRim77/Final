using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 4f;
    public float height = 1.5f;
    public float mouseSensitivity = 200f;

    float yaw;
    float pitch;

    public bool canRotate = true;   // ⭐ เพิ่มตัวนี้

    void Start()
    {
        MouseManager.Instance.LockMouse();
    }

    void LateUpdate()
    {
        if (!target) return;

        if (canRotate)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -20f, 70f);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.2f);
    }
}
