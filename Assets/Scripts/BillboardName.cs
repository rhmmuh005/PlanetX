using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BillboardName : NetworkBehaviour
{
    [SyncVar]
    public Color cc;

    // TextMesh object
    public TextMesh tm;
    // Update is called once per frame
    void Update()
    {
        tm.transform.LookAt(2 * tm.transform.position - Camera.main.transform.position);
       
        tm.color = cc;
    }
}
