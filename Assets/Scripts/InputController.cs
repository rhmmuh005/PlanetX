using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public float vertical;
    public float horizontal;

    public Vector2 mouse_input;

    public bool Fire1;
    public bool Fire2;

    public bool IsWalking;
    public bool IsRunning;
    public bool IsPickUp;
    public bool IsRolling;
    public bool IsReloading;

    // test
    public bool IsDead;

    public void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        mouse_input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");

        IsWalking = Input.GetKey(KeyCode.X);
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsPickUp = Input.GetKeyDown(KeyCode.E);
        IsRolling = Input.GetKey(KeyCode.Space);
        IsReloading = Input.GetKeyDown(KeyCode.R);
    }
}
