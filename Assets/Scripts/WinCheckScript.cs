using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheckScript : MonoBehaviour {
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
            //Log to console that The is only one player left alive.
            Debug.Log("winner");
    }
}
