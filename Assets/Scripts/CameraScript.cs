using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    public Transform playerTransform;
    private Vector3 _CameraOffset;
    public Camera PlayerCamera;

    // Local Health object
    LocalHealth lh;

    // Health Object
    Health h;

    // Ammo counter object
    AmmoCounter ammoCounter;
<<<<<<< HEAD

    // Weapon Reloader object
=======
    PlayerList pl;
>>>>>>> master
    WeaponReloader weaponReloader;

    [Range(0.1f, 1.0f)]
    public float SmoothFactor = 0.5f;

	// Use this for initialization
	void Start () {
        PlayerCamera = Camera.main; //FindObjectOfType<Camera>();
        PlayerCamera.transform.position = new Vector3(playerTransform.position.x, 15, playerTransform.position.z - 5);
        _CameraOffset = PlayerCamera.transform.position - playerTransform.position;

        lh = PlayerCamera.GetComponent<LocalHealth>();
        h = GetComponent<Health>();

        ammoCounter = PlayerCamera.GetComponent<AmmoCounter>();
        pl = PlayerCamera.GetComponent<PlayerList>();
        weaponReloader = GetComponent<WeaponReloader>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        // having the camera follow the player
        Vector3 newPos = playerTransform.position + _CameraOffset;

        PlayerCamera.transform.position = Vector3.Slerp(PlayerCamera.transform.position, newPos, SmoothFactor);
	}

    // updates the UI for health bar (remaining health) and ammo counter (remaining ammo)
    private void Update()
    {
        lh.updateHealthBar(h.health);
        ammoCounter.updateAmmoCounter(weaponReloader);
<<<<<<< HEAD
        
=======
        pl.UpdateText();
>>>>>>> master
    }
}
