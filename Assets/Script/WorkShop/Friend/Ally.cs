using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Ally : Character
{
    public Transform _player;
    public Player player;
    public float followDistance = 3f;
    public float attackRange = 2f;
    public float detectRange = 10f;
    public int bondPoints = 0;
    public event System.Action<Ally> OnBondChanged;


    NavMeshAgent agent;
    private Character currentEnemy;
    private AllyCommand currentCommand = AllyCommand.Follow;

    void Start()
    {
        SetUP();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = movementSpeed;
    }

    void Update()
    {
        if (player == null || _player == null) return;

        // 1. ตรวจว่ามีศัตรูรึเปล่า
        if (currentEnemy != null)
        {
            HandleEnemy(); // ไล่และโจมตีศัตรู
        }
        else
        {
            // 2. ถ้าไม่มีศัตรู ? กลับไปตาม Player
            FollowPlayer();

            // 3. ตรวจหาศัตรูใหม่รอบตัว
            DetectEnemy();
        }

        // 4. ถ้าอยู่ในโหมดเก็บของ
        if (currentCommand == AllyCommand.CollectItem)
            TryCollectNearbyItem();

        //if (player == null) return;
        //HandleEnemy();
        //FollowPlayer();

        //if (currentCommand == AllyCommand.CollectItem)
        //    TryCollectNearbyItem();
    }

    private void HandleEnemy()
    {
        if (currentEnemy == null) DetectEnemy();
        if (currentEnemy != null)
        {
            float dist = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (dist <= attackRange)
            {
                agent.isStopped = true;
                animator.SetTrigger("Trigger");
                currentEnemy.TakeDamage(Damage);
            }
            else if (dist <= detectRange)
            {
                agent.isStopped = false;
                agent.SetDestination(currentEnemy.transform.position);
            }
            else currentEnemy = null;
        }

    
    }


    public void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > followDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity", 1);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Moving", false);
            animator.SetFloat("Velocity", 0);
        }
    }

    private void DetectEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);
        foreach (var hit in hits)
        {
            Character enemy = hit.GetComponent<Character>();
            if (enemy != null && enemy != this && enemy != player)
            {
                currentEnemy = enemy;
                break;
            }
        }
    }

    private void TryCollectNearbyItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hit in hits)
        {
            Item item = hit.GetComponent<Item>();
            if (item != null)
            {
                player.AddItem(item);
                Destroy(item.gameObject);
                ModifyBond(5);
                currentCommand = AllyCommand.Follow;
                break;
            }
        }
    }

    public void SetCommand(AllyCommand cmd) => currentCommand = cmd;
    public void ModifyBond(int delta)
    {
        bondPoints = Mathf.Clamp(bondPoints + delta, 0, 100);
        OnBondChanged?.Invoke(this);
    }

    public void SetTarget(Character enemy)
    {
        currentEnemy = enemy;
        currentCommand = AllyCommand.Attack;
    }
}
