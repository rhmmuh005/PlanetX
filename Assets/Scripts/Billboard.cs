using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Billboard : MonoBehaviour {
   
    // Update is called once per frame
    void Update () {
        
       transform.LookAt(Camera.main.transform);
	}
}
