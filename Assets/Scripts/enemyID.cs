using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class enemyID : NetworkBehaviour
{

    [SyncVar] public string EnemyUniqueName;

    [SyncVar]
    public string EnemyName = "enemy Doe";

    private NetworkInstanceId PlayerNetID;
    private Transform myTransform;

    // Use this for initialization
    private void Start()
    {
       GetNetIdentity();
       SetIdentity();
    }
    void Awake()
    {

        myTransform = transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (myTransform.tag == "Enemy")
        {
            SetIdentity();
        }
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
        if (!isLocalPlayer)
        {
            myTransform.name = EnemyUniqueName;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        string uniqueName = "Enemy " + PlayerNetID.ToString();
        return uniqueName;
    }

    //[Command]
    void CmdTellServerMyIdentity(string name)
    {
        EnemyUniqueName = name;
    }
}
