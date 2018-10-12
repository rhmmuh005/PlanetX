using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    public override void OnStartServer()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        int startPoint = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int num = i + startPoint;
            if (num > spawnPoints.Length - 1) num -= spawnPoints.Length;
            var spawnPosition = spawnPoints[num].transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f));

            var spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}
