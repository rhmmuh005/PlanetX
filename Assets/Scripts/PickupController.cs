using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public enum PickupType
    {
        AMMO,
        RANGE,
        FIRE_RATE
    }

    public PickupType pickupType;
    public int amount;

    public void setAttributes (PickupType type, int amount)
    {
        this.pickupType = type;
        this.amount = amount;
    }

    // Use this for initialization
    void Start () {
		                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
