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

<<<<<<< HEAD
    // player must stand on the ammo pick up and press E to be able to pick up the ammo
=======
>>>>>>> master
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player gets " + this.amount + "ammp");
            other.gameObject.GetComponent<WeaponReloader>().addAmmo(amount);
            PickupSpawner.Destroy(this.gameObject);
        }
    }
}
