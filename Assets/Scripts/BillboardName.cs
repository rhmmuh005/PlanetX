using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BillboardName : NetworkBehaviour
{
    [SyncVar]
    public Color cc;

    public TextMesh tm;
    // Update is called once per frame
    void Update()
    {
        /*if (isLocalPlayer)
        {*/
            tm.transform.LookAt(2 * tm.transform.position - Camera.main.transform.position);
        //}
       
        tm.color = cc;




        /*Vector3 v= Camera.main.transform.position - transform.position;
        v.x = 
        v.z = 0.0f;
        v.y = v.y - 10f;
        transform.LookAt(Camera.main.transform.position - v);*/
    }
}
