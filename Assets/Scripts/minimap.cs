using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour {
    
    public Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }
    private void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = cam.transform.position.y;
        cam.transform.position = newPosition;
    }
}
