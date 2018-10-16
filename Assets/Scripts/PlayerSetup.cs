using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;
    //disables components that should only be running for local player.
    //applied to non-local player on local client

    private void Start()
    {
        if(!isLocalPlayer)
        {
            for(int i =0; i<componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }

}
