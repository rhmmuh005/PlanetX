﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {

    // Text object
    [SerializeField] Text text;

    // weapon reloader
    private WeaponReloader reloader;

    // ammo counter update to keep track of how much ammo is left
    public void updateAmmoCounter(WeaponReloader reloader)
    {
        int amountInInventory = reloader.RoundsRemainingInInventory;
        int amountInClip = reloader.RoundsRemainingInClip;

        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
