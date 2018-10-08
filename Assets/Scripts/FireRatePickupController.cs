using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePickupController : PickupController
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
            Debug.Log("Player range incrases by " + this.amount);
            other.gameObject.GetComponent<GunScript>().addFireRate(amount);
            PickupSpawner.Destroy(this.gameObject);
        }
    }
}