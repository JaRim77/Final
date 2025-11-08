using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    private NavMeshAgent agent;
    Animator animator;

    public GameObject objectToFollow;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, objectToFollow.transform.position);

        if (distance < 5)
        {
            agent.isStopped = true;
            animator.SetInteger("Idle", 0);
        }
        else if (distance >= 5 && distance < 13)
        {
            agent.isStopped = false;
            agent.SetDestination(objectToFollow.transform.position);
            animator.SetInteger("Walk", 1);

            agent.speed = 3;
        }
        else if (distance < 13)
        {
            agent.isStopped = false;
            agent.SetDestination(objectToFollow.transform.position);
            animator.SetInteger("Run", 2);

            agent.speed = 6;
        }

    }
}

        