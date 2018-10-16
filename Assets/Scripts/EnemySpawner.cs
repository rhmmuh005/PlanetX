using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    // set of spawn points stored in an array to spawn enemies at these locations
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    static Text text;
    static GameObject textObject;

    // spawn state to decide when its time to spawn or wait
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] target;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    //public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountDown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    // 
    public override void OnStartServer()
    {
        textObject = GameObject.Find("WaveNumber");
        if (textObject)
        {
            text = textObject.GetComponent<Text>();
            text.enabled = false;
        }

        if (spawnPoints.Length == 0)
        {

        }

        waveCountDown = timeBetweenWaves;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!TargetDestroyed())
            {
                WaveCompleted();
                return;
            }

            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            text.text = "Wave " + (nextWave + 1);
            text.enabled = true;
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }

        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        nextWave++;
    }

    bool TargetDestroyed()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnTarget(_wave.target);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        text.enabled = false;
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnTarget(Transform[] _target)
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Waypoint");

        GameObject _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var enemy = (GameObject)Instantiate(enemyPrefab, _sp.transform.position, _sp.transform.rotation);
        NetworkServer.Spawn(enemy);
    }
}
