using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalHealth : MonoBehaviour {
    //GameObject go = GameObject.Find("health bar");

    public Image healthbar;// = go.GetComponent<SpriteRenderer>().sprite;
    public Text ratiotext;

    float maxHP = 100;

	// Use this for initialization
	void Start () {
        //updateHealthBar();
	}
	
	// Update is called once per frame
	void Update () {
       // updateHealthBar();
	}

    public void updateHealthBar(float h)
    {
        float ratio = h/maxHP;
        healthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratiotext.text = (ratio * 100).ToString() + '%';
    }
}
