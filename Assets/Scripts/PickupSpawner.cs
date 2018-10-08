using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupSpawner : NetworkBehaviour
{
    public static void spawn(GameObject toSpawn)
    {
        Debug.Log("Spawning: " + toSpawn.name);
        NetworkServer.Spawn(toSpawn);
    }
}
