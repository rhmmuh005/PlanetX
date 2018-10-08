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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player range incrases by " + this.amount);
            other.gameObject.GetComponent<GunScript>().addFireRate(amount);
            PickupSpawner.Destroy(this.gameObject);
        }
    }
}