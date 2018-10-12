using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float walkSpeed;
    public float runSpeed;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Rigidbody playerRigidbody;
    private Camera mainCamera;
    Health player_health;

    InputController playerInput;

	// Use this for initialization
	void Start () {
        //set rigidbody to the one that is attached to player
        playerRigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;//FindObjectOfType<Camera>();
        player_health = GetComponent<Health>();

        playerInput = GetComponent<InputController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!(player_health.isDead))
        {
            Move();
            point();
        }

        else
        {
            moveVelocity = new Vector3(0, 0, 0);
            GetComponent<WinCheckScript>().CheckIfWin();
            playerRigidbody.isKinematic = false;
        }
	}

    void Move()
    {
        if(playerInput == null)
        {
            return;
        }
        float moveSpeed = walkSpeed;
        
        if (playerInput.IsRunning)
            moveSpeed = runSpeed;

        if ((playerInput.IsRunning) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)))
            moveSpeed = walkSpeed;

        moveInput = new Vector3(playerInput.horizontal, 0f, playerInput.vertical);
        moveVelocity = ((moveInput.z * transform.forward) + (moveInput.x * transform.right));
        moveVelocity.Normalize();
        moveVelocity *= moveSpeed;
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = moveVelocity;
    }

    private void point()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (GroundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //test
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
