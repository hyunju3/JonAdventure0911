using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    Vector3 orginPosition;
    NavMeshAgent agent;

    void Start()
    {
        // Cache agent component and destination.
        orginPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void Update()
    {
        // Update destination if the target moves one unit
        if (Vector3.Distance(destination, target.position) < 5.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
        else
        {
            
        }
    }
}
