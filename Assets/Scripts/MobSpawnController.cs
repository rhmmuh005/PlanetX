using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

public class MobSpawnController : NetworkBehaviour
{

    public GameObject neutralPrefab;
    public bool gameOver = true;
    private Vector3 spawnPoint;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    public override void OnStartServer()
    {
        this.spawnNPCs(5);
    }

    public void spawnNPCs(int numToSpawn)
    {
        spawnPoint = GetComponent<Transform>().position;

        for (int i = 0; i < numToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(spawnPoint.x + Random.Range(-2.0f, 2.0f), spawnPoint.y + 1.0f, spawnPoint.z + Random.Range(-2.0f, 2.0f));
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0);

            GameObject enemy = (GameObject)Instantiate(neutralPrefab, spawnPos, spawnRotation);

            enemy.name = getNewEnemyName();

            NetworkServer.Spawn(enemy);
        }
    }


    public string getNewEnemyName()
    {
        string newName = "";
        for (int i = 0; i < 32; i++)
        {
            newName += glyphs[Random.Range(0, glyphs.Length)];
        }

        return newName;
    }
}