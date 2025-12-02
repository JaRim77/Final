using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TPSMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 6f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;   // ไม่ให้ตัวล้ม
    }

    void Update()
    {
        // ตรวจพื้น
        GroundCheck();

        // กระโดดเมื่อกด Space + อยู่บนพื้น
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // --- การเดินแบบ TPS ตามมุมกล้อง ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cam.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDir = (forward * v + right * h).normalized;

        // เคลื่อนที่
        Vector3 vel = moveDir * moveSpeed;
        vel.y = rb.linearVelocity.y; // แกน Y ให้ฟิสิกส์จัดการ
        rb.linearVelocity = vel;

        // หมุนตามทิศทางเดิน
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // อนิเมชันเดิน
        if (animator != null)
            animator.SetFloat("Blend", new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude / moveSpeed);
    }

    void GroundCheck()
    {
        // ยิง Ray ลงพื้นใต้เท้า
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        float checkDistance = 0.3f;

        isGrounded = Physics.Raycast(ray, checkDistance, groundMask);

        if (animator != null)
            animator.SetBool("Grounded", isGrounded);
    }



    void Jump()
    {
        // รีเซ็ตความเร็ว Y ก่อนกระโดด
        Vector3 vel = rb.linearVelocity;
        vel.y = 0;
        rb.linearVelocity = vel;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (animator != null)
            animator.SetTrigger("Jump");
    }
}
