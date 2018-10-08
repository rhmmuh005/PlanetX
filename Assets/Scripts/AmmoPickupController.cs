using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupController : PickupController {

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player gets " + this.amount + "ammp");
            other.gameObject.GetComponent<WeaponReloader>().addAmmo(amount);
            PickupSpawner.Destroy(this.gameObject);
        }
    }
}
