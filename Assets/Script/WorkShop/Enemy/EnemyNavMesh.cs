using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonsterGO
{
    public float detectRange = 8f;
    public float attackRange = 1.8f;
    public float attackCooldown = 1.2f;

    private float lastAttackTime = 0f;

    private NavMeshAgent agent;
    private Vector3 startPoint;
    private bool startPointSet = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.speed = movementSpeed;
        agent.stoppingDistance = attackRange - 0.5f;

        StartCoroutine(SetStartPoint());
    }

    private System.Collections.IEnumerator SetStartPoint()
    {
        yield return null;
        startPoint = transform.position;
        startPointSet = true;
    }

    private void Update()
    {
        if (!startPointSet || player == null) return;

        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        float distToStart = Vector3.Distance(transform.position, startPoint);
        float agentVel = agent.velocity.magnitude;

        // ---------------- ไล่ผู้เล่น ----------------
        if (distToPlayer < detectRange)
        {
            // ถ้าอยู่ไกลเกิน attackRange ให้เดินไล่
            if (distToPlayer > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
                animator.SetFloat("Speed", 1);
            }
            else
            {
                // ---------------- โจมตี ----------------
                agent.isStopped = true;
                animator.SetFloat("Speed", 0);

                // หันหน้าเข้าผู้เล่น
                Vector3 dir = player.transform.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

                TryAttackPlayer();
            }

            return;
        }

        // ---------------- กลับจุดเกิด ----------------
        agent.isStopped = false;
        agent.SetDestination(startPoint);

        if (distToStart < 1f || agentVel < 0.05f)
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
            return;
        }

        animator.SetFloat("Speed", 1);
    }

    // ---------------- ฟังก์ชันโจมตี ----------------
    void TryAttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Attack");  // ใช้อนิเมชันจาก Character ได้เลย
            player.TakeDamage(Damage);      // ใช้พลังโจมตีจาก Character
            lastAttackTime = Time.time;

            Debug.Log($"{Name} attacked player for {Damage} damage.");
        }
    }
}
