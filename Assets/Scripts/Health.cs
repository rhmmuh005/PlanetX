using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public RectTransform healthBar;
    //Animator animator;
    public bool isDead = false;
    //WinCheckScript ws;
    public GameObject[] gos;

    [SyncVar(hook = "OnChangeHealth")]
    public int health = 100;

    // Use this for initialization
    void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        //ws = GetComponent<WinCheckScript>();
    }
    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            health = 0;

        /*if(gameObject.tag == "Player")
            ws.CheckIfWin();*/
        //check if there is only one player left alive

        /*
        if (health <= 0 && !isDead)
        {
            animator.SetTrigger("IsDead");
            isDead = true;
        }
        */
    }

    void OnChangeHealth(int h)
    {
        health = h;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    public void UpdateText()
    {

        //create an array of players
        gos = GameObject.FindGameObjectsWithTag("Player");
        string s = "";
        int count = 0;

        //check each players health and create a list of players still alive to be shown when press f
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<Health>().health > 0)
            {
                s = s +
                gos[i].GetComponent<PlayerID>().PlayerName + "\n";
                count++;
            }
        }
        Camera.main.GetComponent<LocalHealth>().playerList.text = s;

        //display appropriate messages.
        if (count <= 1 && gos.Length!=1)
        {
            if (health <= 0)
            {
                Camera.main.GetComponent<LocalHealth>().updateWinPanel("You Lose");
            }
            else
                Camera.main.GetComponent<LocalHealth>().updateWinPanel("You Win");

        }
        else
            Camera.main.GetComponent<LocalHealth>().updateWinPanel("");

    }

    private void Update()
    {
        //Update win/lose message for local player depending on health levels.
        if(isLocalPlayer)
        {
            UpdateText();
           
        }
    }
}
