using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour
{
    public enum States
    {
        Chase, Patrol, Attack, Idle
    }

    List<Transform> wayPoints = new List<Transform>();
    private Transform player;
    public NavMeshAgent agent;
    [SerializeField] float chaseRange, attackRange, patrolRange;
    [SerializeField] float patrolSpeed, chaseSpeed;
    public States currentState = States.Idle;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {      
            Transform wayPointObjects = GameObject.FindGameObjectWithTag("WayPoints").transform;
            foreach (Transform t in wayPointObjects)
            {
                wayPoints.Add(t);
            }
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
 
    }
    private void Update()
    {    
            CheckDistanceToPlayer();
 
    }
    private void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, this.transform.position);
        if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            
            currentState = States.Chase;
            agent.speed = chaseSpeed;
        }
        else if (distanceToPlayer <= attackRange)
        {
          
            currentState = States.Attack;
        }
        else if (distanceToPlayer > chaseRange)
        {
            
            currentState = States.Patrol;
            agent.speed = patrolSpeed;
        }
    }

    public void SetRandomDestinationPoint()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRange);
    }
}
