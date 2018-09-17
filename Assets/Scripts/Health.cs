using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public RectTransform healthBar;
    //Animator animator;
    public bool isDead = false;
    WinCheckScript ws;

    [SyncVar(hook = "OnChangeHealth")]
    public int health = 100;

    // Use this for initialization
    void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        ws = GetComponent<WinCheckScript>();
    }
    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            health = 0;

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

        if (isLocalPlayer)
            ws.CheckIfWin();
    }
}
