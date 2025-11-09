using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Transform RightHand; 
    public Transform LeftHand;

    public List<Item> inventory = new List<Item>();
    Vector3 _inputDirection;
    bool _isAttacking = false;
    bool _isInteract = false;

    public List<Ally> allies = new List<Ally>();
    int currentAllyIndex = -1;
    public float swapCooldown = 0.3f;
    float lastSwapTime = -10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = maxHealth;

        if (allies.Count > 0)
        {
            foreach (var a in allies) a.player = this;
            currentAllyIndex = 0;
        }
    }

    void FixedUpdate()
    {
        Move(_inputDirection);
        Turn(_inputDirection);
        Attack(_isAttacking);
        Interact(_isInteract);
    }

    void Update() => HandleInput();

    private void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        _inputDirection = new Vector3(x, 0, y);

        if (Input.GetMouseButtonDown(0)) _isAttacking = true;
        if (Input.GetKeyDown(KeyCode.E)) _isInteract = true;

        if (Input.GetKeyDown(KeyCode.Tab)) TrySwapAlly();
        if (Input.GetKeyDown(KeyCode.F)) CommandAllies(AllyCommand.Attack);
        if (Input.GetKeyDown(KeyCode.G)) CommandAllies(AllyCommand.Wait);
        if (Input.GetKeyDown(KeyCode.H)) CommandAllies(AllyCommand.CollectItem);
        if (Input.GetKeyDown(KeyCode.V)) TryDualAttack();
    }

    public void AddItem(Item item) => inventory.Add(item);

    void TrySwapAlly()
    {
        if (Time.time - lastSwapTime < swapCooldown) return;
        lastSwapTime = Time.time;
        if (allies.Count <= 1) return;
        currentAllyIndex = (currentAllyIndex + 1) % allies.Count;
        Debug.Log($"Switched to Ally: {allies[currentAllyIndex].name}");
    }

    void CommandAllies(AllyCommand cmd)
    {
        foreach (var a in allies)
            if (a != null && a.IsAlive)
                a.SetCommand(cmd);
    }

    void TryDualAttack()
    {
        if (allies.Count < 1) return;
        Ally ally = allies[currentAllyIndex];
        if (ally.bondPoints >= 50)
        {
            animator.SetTrigger("DualAttack");
            ally.Animator.SetTrigger("DualAttack"); // ผ่าน public getter
        }
    }

    public void Attack(bool isAttacking)
    {
        if (!isAttacking) return;
        animator.SetTrigger("Attack");
        var e = InFront as Idestoryable;
        if (e != null)
        {
            e.TakeDamage(Damage);
        }
        _isAttacking = false;
    }

    private void Interact(bool interactable)
    {
        if (!interactable) return;
        IInteractable e = InFront as IInteractable;
        e?.Interact(this);
        _isInteract = false;
    }

}
