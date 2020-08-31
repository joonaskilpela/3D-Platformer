using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10;
    public float rotSpeed = 5;
    Transform cam;

    private void Start()
    {
        cam = Transform.FindObjectOfType<CameraFollow>().transform;
    }
    private void FixedUpdate()
    {
        float forward = Input.GetAxis("Vertical");
        float side = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * forward * speed * Time.fixedDeltaTime);
        transform.Rotate(transform.rotation.x, (side * rotSpeed), transform.rotation.z);
    }
}
