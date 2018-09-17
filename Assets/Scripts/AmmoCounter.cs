using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {

    [SerializeField] Text text;

    private WeaponReloader reloader;

    private void Reloader_OnAmmoChanged()
    {
        int amountInInventory = reloader.RoundsRemainingInInventory;
        int amountInClip = reloader.RoundsRemainingInClip;

        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }

    // Use this for initialization
    void Start () {
        reloader = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponReloader>();
        reloader.OnAmmoChanged += Reloader_OnAmmoChanged;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
