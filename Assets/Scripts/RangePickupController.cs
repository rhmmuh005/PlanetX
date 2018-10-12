﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePickupController : PickupController
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player clip size incrases by " + this.amount);
            other.gameObject.GetComponent<WeaponReloader>().increaseClipSize(amount);
            PickupSpawner.Destroy(this.gameObject);
        }
    }
}