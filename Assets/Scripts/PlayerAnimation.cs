using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimation : NetworkBehaviour
{

    Animator animator;
    NetworkAnimator networkAnim;
    Health playerHealth;
    GunScript shootScript;

    InputController input_controller;

    public bool canShoot = true;
    public bool canRoll = true;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        networkAnim = GetComponent<NetworkAnimator>();
        playerHealth = GetComponent<Health>();
        shootScript = GetComponent<GunScript>();

        input_controller = GetComponent<InputController>();

        animator.SetLayerWeight(animator.GetLayerIndex("Reloading"), 0);
        animator.SetLayerWeight(animator.GetLayerIndex("PickUp"), 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerHealth.isDead)
        {
            PickUp();
            animator.SetBool("IsRunning", input_controller.IsRunning);
            Rolling();
            Fire();
            //Reload();
            Death();
        }
    }

    private void PickUp()
    {
        if (input_controller.vertical == 0.0 && input_controller.horizontal == 0.0 && input_controller.IsPickUp)
        {
            animator.SetLayerWeight(animator.GetLayerIndex("PickUp"), 1);
            networkAnim.SetTrigger("TestPickUp");
            StartCoroutine(PickUpDelay());
        }

        else
        {
            animator.SetFloat("Vertical", input_controller.vertical);
            animator.SetFloat("Horizontal", input_controller.horizontal);
        }
    }

    private void Rolling()
    {
        if (input_controller.IsRolling && input_controller.IsRunning)
        {
            canShoot = false;
            networkAnim.SetTrigger("TestRoll");
            StartCoroutine(RollDelay());
        }
    }

    private void Fire()
    {
        if (input_controller.Fire1 && canShoot && !(input_controller.IsRunning) && !(input_controller.IsPickUp))
        {

            if (animator.GetLayerWeight(animator.GetLayerIndex("Reloading")) == 0)
            {
                if (shootScript.canFire)
                    networkAnim.SetTrigger("IsShooting");
            }
        }
    }

    private void Reload()
    {
        if (input_controller.IsReloading && !(input_controller.IsRunning))
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Reloading"), 1);
            networkAnim.SetTrigger("IsReloading");

            StartCoroutine(ReloadDelay());
        }
    }

    private void Death()
    {
        if (playerHealth.health <= 0 && !(playerHealth.isDead))
        {
            networkAnim.SetTrigger("IsDead");
            playerHealth.isDead = true;
        }
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(1f);
        animator.SetLayerWeight(animator.GetLayerIndex("Reloading"), 0);
    }

    IEnumerator CanRollDelay()
    {
        yield return new WaitForSeconds(1.5f);
        canRoll = true;
    }

    IEnumerator RollDelay()
    {
        yield return new WaitForSeconds(1.15f);
        canShoot = true;
    }

    IEnumerator PickUpDelay()
    {
        yield return new WaitForSeconds(1f);
        animator.SetLayerWeight(animator.GetLayerIndex("PickUp"), 0);
    }

    public void FootStep()
    {

    }

    public void EndPickup()
    {

    }

    public void RollSound()
    {

    }

    public void CantRotate()
    {

    }

    public void EndRoll()
    {

    }
}
