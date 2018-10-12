using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour {
    public Text t;
    public GameObject[] gos;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // UpdateText();
	}

    public void UpdateText()
    {
        gos = GameObject.FindGameObjectsWithTag("Player");
        string s = "";

        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<Health>().health > 0)
            {
                s = s + 
                gos[i].GetComponent<PlayerID>().PlayerName +"\n";
            }
        }
        t.text = s;
    }
}
