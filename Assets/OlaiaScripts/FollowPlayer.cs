using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2f;

    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
    

    public void Follow()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }
}