using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponReloader : NetworkBehaviour
{

    [SerializeField] int maxAmmo;
    [SerializeField] int clipSize;
    [SerializeField] Container inventory;

    public Container container;

    public int shotsFiredInClip;
    bool isReloading;

    public event System.Action OnAmmoChanged;

    System.Guid containerItemId;

    Animator animator;
    NetworkAnimator networkAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
        networkAnim = GetComponent<NetworkAnimator>();
        container = GetComponentInChildren<Container>();

        containerItemId = inventory.Add(this.name, maxAmmo);
    }

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
    {
        if (isReloading)
            return;

        if (container.items[0].GetAmountTaken == maxAmmo)
        {
            print("Out of ammo");
            return;
        }

        else
        {
            isReloading = true;

            int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip);

            print("Reload started");
            ExecuteReload(amountFromInventory);

            animator.SetLayerWeight(animator.GetLayerIndex("Reloading"), 1);
            networkAnim.SetTrigger("IsReloading");

            StartCoroutine(ReloadDelay());
        }

        print("Amount taken is " + container.items[0].GetAmountTaken);
    }

    private void ExecuteReload(int amounts)
    {
        print("Reload executed");
        isReloading = false;

        shotsFiredInClip -= amounts;

        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }

    public void TakeFromClip(int amount)
    {
        //print("taking from clip");
        shotsFiredInClip += amount;
        print("Shots fired in clip is: " + shotsFiredInClip);

        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(1f);
        animator.SetLayerWeight(animator.GetLayerIndex("Reloading"), 0);
    }

    public void addAmmo(int amount)
    {
        inventory.addMoreAvailableItems(containerItemId, amount);
    }

    public void increaseClipSize(int amount)
    {
        this.clipSize += amount;
        this.addAmmo(clipSize - shotsFiredInClip);
        this.shotsFiredInClip = 0;
    }
}
