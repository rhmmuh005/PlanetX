using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {
    //public Image playersList;
    public Transform t;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.F))
        {
            //Debug.Log("working");
            t.gameObject.SetActive(true);
        }
        else
        {
            t.gameObject.SetActive(false);
        }
        
	}
}
