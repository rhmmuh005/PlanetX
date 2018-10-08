using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour {

    public enum State
    {
        IDLE,
        CHASE,
        PATROL
    }

    public State currentState;

    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 10.0f;

    public float chaseSpeed = 15f;
    public GameObject target;

    Animator animator;

    public NavMeshAgent agent;
    public GameObject[] pickups;

    GameObject[] players;
    Transform closestPlayer;
    Health NPChealth;


	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
		NPChealth = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        currentState = State.PATROL;


        //StartCoroutine(FSM());
	}
	
	// Update is called once per frame
	void Update () {
        /*
        players = GameObject.FindGameObjectsWithTag("Player");
        closestPlayer = GetClosestEnemy(players);

        if (NPChealth.health > 0 && closestPlayer != null)
        {
            agent.SetDestination(closestPlayer.position);
        }

        else
        {
            if (this.transform.GetComponent<Health>().health <= 0)
            {
                agent.enabled = false;
                NPCDestroy();
            }
        }
        */

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
            }
        }

        else if (this.transform.GetComponent<Health>().health <= 0 && !(this.transform.GetComponent<Health>().isDead))
        {
            Debug.Log("Enemy is dieing...");
            SpawnPickup();
            //animator.SetBool("IsWalking", false);
            agent.enabled = false;
            animator.SetTrigger("IsDead");
            this.transform.GetComponent<Health>().isDead = true;
        }
    }

    /*
    IEnumerator FSM()
    {
        while (this.transform.GetComponent<Health>().health > 0)
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
            }

            yield return null;
        }
    }
    */

    void Patrol()
    {
        agent.speed = patrolSpeed;
        if (Vector3.Distance (this.transform.position, waypoints[waypointInd].transform.position) >= 2)
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

    void Chase()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        closestPlayer = GetClosestEnemy(players);

        agent.speed = chaseSpeed;
        agent.SetDestination(closestPlayer.position);
    }

    void Die()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(closestPlayer.position);
    }

    Transform GetClosestEnemy(GameObject[] enemies)
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
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = currentTarget;
            }
        }

        return bestTarget;
    }

    private void SpawnPickup()
    {
        GameObject toSpawn = Instantiate(pickups[0], this.transform.position, this.transform.rotation);
        Debug.Log("Spawn Pickup");
        PickupSpawner.spawn(toSpawn);
    }
}
