using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunScript : NetworkBehaviour {

    [SerializeField] float rateOfFire;

    public bool canFire;

    public int damage = 10;
    public float range = 100;
    public float impactForce = 80;

    public AudioSource AS;
    public AudioClip FireSound;


    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    Health player_health;
    public WinCheckScript wc;

    Animator animator;
    PlayerAnimation playerAnimation;
    private WeaponReloader reloader;

    float nextFireAllowed;
    InputController input_controller;

    LineRenderer line;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        reloader = GetComponent<WeaponReloader>();
        AS.clip = FireSound;
        player_health = GetComponent<Health>();

        input_controller = GetComponent<InputController>();

        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (input_controller.Fire1 && playerAnimation.canShoot && !(input_controller.IsRunning) && !(input_controller.IsPickUp))
        {
            //this.GetComponent<PlayerList>().UpdateText();
            if (animator.GetLayerWeight(animator.GetLayerIndex("Reloading")) == 0)
            {
                if (!(player_health.isDead))
                {
                    CmdShoot();
                    line.enabled = true;
                }
            }
        }

        else
        {
            line.enabled = false;
        }

        if (input_controller.IsReloading && !(input_controller.IsRunning))
        {
            if (reloader.shotsFiredInClip == 0)
                return;

            reloader.Reload();
        }

        if (reloader.RoundsRemainingInClip == 0 && reloader.RoundsRemainingInInventory > 0 && !(input_controller.IsRunning))
        {
            if (reloader.shotsFiredInClip == 0)
                return;

            reloader.Reload();
        }
    }

    void CmdShoot()
    {
        canFire = false;
        //line.enabled = true;

        if (Time.time < nextFireAllowed)
            return;

        if (Time.time < nextFireAllowed)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;

            if (reloader.RoundsRemainingInClip == 0)
                return;

            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;
        
        CmdOnShoot();
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            //testing
            Debug.Log("Hit : "+hit.transform.name);

            if(hit.transform.tag =="Player")
            {
                string uIdentity = hit.transform.name;
                CmdTellServerWhoWasShot(uIdentity, damage);
                //wc.CheckIfWin();

            }
            if (hit.transform.tag == "Enemy")
            {
                string uIdentity = hit.transform.name;
                CmdTellServerWhoWasShot(uIdentity, damage);

                hit.transform.LookAt(transform.position, Vector3.up);
                hit.transform.position = Vector3.Lerp(hit.transform.position, transform.position, 0.01f);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            CmdOnHit(hit.point, hit.normal);
        }
        else
        {
            Debug.Log("Miss");
        }

        canFire = true;
        //line.enabled = false;
    }

    [Command]
    void CmdTellServerWhoWasShot(string UniqueID, int dmg)
    {
        GameObject go = GameObject.Find(UniqueID);
        go.GetComponent<Health>().TakeDamage(dmg);

        if(go.tag == "Player")
            go.GetComponent<WinCheckScript>().CheckIfWin();
    }

    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    [ClientRpc]
    void RpcDoShootEffect()
    {
        muzzleFlash.Play();
        //line.enabled = true;

        Debug.Log(gameObject.name +" Play shoot sound");
        
        AS.Play();
    }

    [Command]
    void CmdOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcDoHitEffect(_pos, _normal);
    }

    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        GameObject impactGO = Instantiate(impactEffect, _pos, Quaternion.LookRotation(_normal));
        Destroy(impactGO, 2f);
    }

    public void addRange(int amount)
    {
        this.range += amount;
    }

    public void addFireRate(int amount)
    {
        float newRate = this.rateOfFire - (amount / 100);
        if (newRate < 0.05) newRate = 0.05f;
        this.rateOfFire = newRate;
    }
}
