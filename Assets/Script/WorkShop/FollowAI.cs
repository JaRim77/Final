using UnityEngine;

using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{

    NavMeshAgent agent;

    

    public GameObject objectToFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, objectToFollow.transform.position);

        if (distance < 5)
        {
            agent.isStopped = true;
            

        }
        else if(distance >= 5 && distance < 13)
        {
            agent.isStopped = false;
            agent.SetDestination(objectToFollow.transform.position);
           
            agent.speed = 3;

        }
        else if (distance < 13)
        {
            agent.isStopped = false;
            agent.SetDestination(objectToFollow.transform.position);

            

            agent.speed = 6;
        }
    }
}
