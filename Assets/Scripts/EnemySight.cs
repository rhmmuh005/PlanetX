using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    // Enum for different states
    public enum State
    {
        IDLE,
        CHASE,
        PATROL,
        INVESTIGATE,
        ATTACK
    }

    public State currentState;

    // Patrol

    // array of a set of waypoints
    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 3.0f;

    //Chase
    public float chaseSpeed = 5f;
    public GameObject target;

    // Investigate
    private Vector3 investigateSpot;
    private float timer = 0;
    public float investigateWait = 10;

    // Sight
    public float heightMultiplier;
    public float sightDist = 10;

    //Pickups
    public GameObject[] pickups;


    //Attack
    public float attackRange = 5.0f;
    public int attackDamage = 7;
    private float nextAttackAllowed;
    private float attackRate = 0.25f;
    public Animation attackAnimation;

    Animator animator;

    // NavMesh object for enemy
    public NavMeshAgent agent;

    GameObject[] players;
    Transform closestPlayer;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        animator = GetComponentInChildren<Animator>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        currentState = State.PATROL;

        heightMultiplier = 0.9f;

        nextAttackAllowed = Time.fixedTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.GetComponent<Health>().health > 0)
        {
            switch (currentState)
            {
                case State.PATROL:
                    animator.SetBool("IsWalking", true);
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
        }

        else if (this.transform.GetComponent<Health>().health <= 0 && !(this.transform.GetComponent<Health>().isDead))
        {
            //animator.SetBool("IsWalking", false);
            agent.enabled = false;
            animator.SetTrigger("IsDead");
            this.transform.GetComponent<Health>().isDead = true;
            Debug.Log("Enemy is dieing...");
            GameObject destroyEnemy = this.transform.gameObject;
            Destroy(destroyEnemy, 2f);
            SpawnPickup();
        }
    }

    // Patrol method that makes enemy walk to different waypoints
    void Patrol()
    {
        agent.speed = patrolSpeed;
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
        }

        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            waypointInd = Random.Range(0, waypoints.Length);
        }

        else
        {
            currentState = State.IDLE;
        }
    }

    // chase method that makes enemy follow closest player
    void Chase()
    {
        timer += Time.deltaTime;
        if (target.gameObject.GetComponent<Health>().isDead || (timer > investigateWait))
        {
            currentState = State.PATROL;
            timer = 0;
        }
        else
        {
            animator.SetBool("IsRunning", true);
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
        }
    }

    // enemy attacks player if he/she is in a certain range
    void Attack()
    {
        Vector3 directionToTarget = target.transform.position - this.transform.position;
        float dSqrToTarget = directionToTarget.sqrMagnitude;
        if (dSqrToTarget > attackRange || target.GetComponent<Health>().isDead)
        {
            currentState = State.CHASE;
        }
        else
        {
            if (Time.time < nextAttackAllowed)
                return;

            agent.SetDestination(target.transform.position);
            target.gameObject.GetComponent<Health>().TakeDamage(this.attackDamage);
            animator.SetTrigger("IsAttacking");
            nextAttackAllowed = Time.time + attackRate;
        }
    }

    // enemies use raycast as a sight to view the world and notice players
    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.red);

        players = GameObject.FindGameObjectsWithTag("Player");
        closestPlayer = GetPlayerIfInAttackRange(players, attackRange);
        if (closestPlayer != null)
        {
            currentState = State.ATTACK;
            target = closestPlayer.gameObject;
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                currentState = State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                currentState = State.CHASE;
                target = hit.collider.gameObject;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                currentState = State.CHASE;
                target = hit.collider.gameObject;
            }
        }
    }

    // enemies find players who are in an attack range and can thus launch attacks
    Transform GetPlayerIfInAttackRange(GameObject[] enemies, float attackRange)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        Transform currentTarget;
        foreach (GameObject potentialTarget in enemies)
        {
            currentTarget = potentialTarget.transform;
            Vector3 directionToTarget = currentTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget < attackRange)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = currentTarget;
            }
        }

        return bestTarget;
    }

    // pick up is spawned at the same position where enemy died
    private void SpawnPickup()
    {
        GameObject toSpawn = Instantiate(pickups[Random.Range(0, 2)], this.transform.position, this.transform.rotation);
        Debug.Log("Spawn Pickup");
        PickupSpawner.spawn(toSpawn);
    }
}
