using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform playerTransform;
    private Vector3 _CameraOffset;
    public Camera PlayerCamera;

    LocalHealth lh;
    Health h;

    [Range(0.1f, 1.0f)]
    public float SmoothFactor = 0.5f;

	// Use this for initialization
	void Start () {
        PlayerCamera = Camera.main; //FindObjectOfType<Camera>();
        PlayerCamera.transform.position = new Vector3(playerTransform.position.x, 15, playerTransform.position.z - 5);
        _CameraOffset = PlayerCamera.transform.position - playerTransform.position;

        lh = PlayerCamera.GetComponent<LocalHealth>();
        h = GetComponent<Health>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 newPos = playerTransform.position + _CameraOffset;

        PlayerCamera.transform.position = Vector3.Slerp(PlayerCamera.transform.position, newPos, SmoothFactor);
	}
    private void Update()
    {
        lh.updateHealthBar(h.health);
    }
}
