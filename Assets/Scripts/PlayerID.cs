using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour {

    [SyncVar] public string PlayerUniqueName;

    [SyncVar]
    public string PlayerName = "John Doe";

    private NetworkInstanceId PlayerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }


    // Use this for initialization
    void Awake() {

        myTransform = transform;

    }

    // Update is called once per frame
    void Update() {

        if (myTransform.tag == "Player")
        {
            SetIdentity();
            this.GetComponentInChildren<TextMesh>().text = PlayerName;
        }

        /*
        if(myTransform.name == "" || myTransform.name == "PlayerCapsule(Clone)" || myTransform.name == "Robot(Clone)" || myTransform.name == "SciFiPlayer(Clone)")
        {
            SetIdentity();
        }
        */

    }

    [Client]
    void GetNetIdentity()
    {
        PlayerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    [Client]
    void SetIdentity()
    {
        if(!isLocalPlayer)
        {
            myTransform.name = PlayerUniqueName;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        string uniqueName = "Player " + PlayerNetID.ToString();
        return uniqueName;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        PlayerUniqueName = name;
    }
}
