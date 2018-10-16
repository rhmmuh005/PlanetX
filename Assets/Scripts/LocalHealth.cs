using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalHealth : MonoBehaviour {
    //GameObject go = GameObject.Find("health bar");

    public Image healthbar;// = go.GetComponent<SpriteRenderer>().sprite;
    public Text ratiotext;
    public TextMeshProUGUI wintext;

    //used to link the playerlist text to the player list script on the player.
    public Text playerList;


    float maxHP = 100;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}


    //show health in local HUD healthbar
    public void updateHealthBar(float h)
    {
        float ratio = h/maxHP;
        healthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratiotext.text = (ratio * 100).ToString() + '%';
    }

    public void updateWinPanel(string s)
    {
        wintext.SetText(s);
    }
}
