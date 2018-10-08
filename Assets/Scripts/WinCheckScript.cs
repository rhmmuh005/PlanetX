using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WinCheckScript : NetworkBehaviour {
    public GameObject[] gos;
    public int count;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    public void CheckIfWin()
    {
        count = 0;
        gos = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<Health>().health <= 0)
            {
                count++;
            }
        }
        if (count == (gos.Length - 1))
        {
            if(hasAuthority)
            {
                RpcEndMatch();
            }
            else
                CmdOnWinCondition();
        }
            //Log to console that The is only one player left alive.
            //Debug.Log("winner");
    }

    [Command]
    void CmdOnWinCondition()
    {
        RpcEndMatch();
    }

    [ClientRpc]
    void RpcEndMatch()
    {
        if(gameObject.GetComponent<Health>().health <= 10 && isLocalPlayer)
        {
            gameObject.GetComponent<CameraScript>().PlayerCamera.GetComponent<LocalHealth>().updateWinPanel("Wasted");
            Debug.Log(gameObject.name+" You Lost");
            Debug.Log(gameObject.GetComponent<Health>().health+ " Health");
        }
        else if(gameObject.GetComponent<Health>().health <= 10)
        {
            Debug.Log(gameObject.name +" You Win");
            Debug.Log(gameObject.GetComponent<Health>().health + " Health");

            gameObject.GetComponent<CameraScript>().PlayerCamera.GetComponent<LocalHealth>().updateWinPanel("You Win");
        }
    }
}
