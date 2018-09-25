using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour {

    public NavMeshAgent agent;

    GameObject[] players;
    Transform closestPlayer;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        players = GameObject.FindGameObjectsWithTag("Player");
        closestPlayer = GetClosestEnemy(players);

        if (this.transform.GetComponent<Health>().health > 0 && closestPlayer != null)
        {
            agent.SetDestination(closestPlayer.position);
        }

        else
        {
            if (this.transform.GetComponent<Health>().health <= 0)
            {
                agent.enabled = false;
            }
        }
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
}
