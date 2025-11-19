using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Hand setting")]
    public Transform RightHand;
    public Transform LeftHand;
    public List<Item> inventory = new List<Item>();

    Vector3 _inputDirection;
    bool _isAttacking = false;
    bool _isInteract = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    public void FixedUpdate()
    {
        Move(_inputDirection);
        Turn(_inputDirection);
        Attack(_isAttacking);
        Interact(_isInteract);
    }
    public void Update()
    {
        HandleInput();
    }
    public void AddItem(Item item) {
        inventory.Add(item);
    }

    private void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        // 1) ดึงทิศกล้องบนพื้น (ไม่รวมความสูง)
        Transform cam = Camera.main.transform;
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        // 2) ประกอบทิศเดินตามกล้อง
        _inputDirection = (camForward * y + camRight * x).normalized;

        // ระบบโจมตี
        if (Input.GetMouseButtonDown(0))
            _isAttacking = true;

        // ระบบกดคุย / interact
        if (Input.GetKeyDown(KeyCode.E))
            _isInteract = true;
    }

    public void Attack(bool isAttacking) {
        if (isAttacking) {
            animator.SetTrigger("Attack");
            var e = InFront as Idestoryable;
            if (e != null)
            {
                e.TakeDamage(Damage);
                Debug.Log($"{gameObject.name} attacks for {Damage} damage.");
            }
            _isAttacking = false;
        }
    }
    private void Interact(bool interactable)
    {
        if (interactable)
        {
            IInteractable e = InFront as IInteractable;
            if (e != null) {
                e.Interact(this);
            }
            _isInteract = false;

        }
    }
    //เพิ่มเติมฟังก์ชันการรักษาและรับความเสียหาย
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        GameManager.Instance.UpdateHealthBar(health, maxHealth);

    }
    public override void Heal(int amount)
    {
        base.Heal(amount);
        GameManager.Instance.UpdateHealthBar(health, maxHealth);

    }

}
