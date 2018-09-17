using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.AI;

public class NpcController : NetworkBehaviour {

	public const int maxHealth = 100;

    public bool destroyOnDeath;

    NavMeshAgent nav;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    GameObject[] players;
    Transform closestPlayer;
    Health enemyHealth;


    void Awake()
    {
        // Set up the references.
        players = GameObject.FindGameObjectsWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<Health>();
    }


    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        closestPlayer = GetClosestEnemy(players);


        // If the enemy and the player have health left...
        if (enemyHealth.health > 0)
        {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(closestPlayer.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }



    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, will be invoked on the Clients
                RpcRespawn();
            }
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the player’s position to origin
            transform.position = Vector3.zero;
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