using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WinCheckScript : NetworkBehaviour {
    public GameObject[] players;
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
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].GetComponent<Health>().isDead)
            {
                count++;
            }
        }
        if (count <= 1)
            CmdOnWinCondition();
        else
            Debug.Log("Players Alive: " + count);
    }


    [Command]
    public void CmdOnWinCondition()
    {
        RpcEndMatch();
    }

    [ClientRpc]
    void RpcEndMatch()
    {
        if(gameObject.GetComponent<Health>().health <= 0 && isLocalPlayer)
        {
            gameObject.GetComponent<CameraScript>().PlayerCamera.GetComponent<LocalHealth>().updateWinPanel("Wasted");
            Debug.Log(gameObject.name+" You Lost");
            Debug.Log(gameObject.GetComponent<Health>().health+ " Health");
        }
        else if(gameObject.GetComponent<Health>().health <= 0)
        {
            Debug.Log(gameObject.name +" You Win");
            Debug.Log(gameObject.GetComponent<Health>().health + " Health");

            gameObject.GetComponent<CameraScript>().PlayerCamera.GetComponent<LocalHealth>().updateWinPanel("You Win");
        }
    }
}
