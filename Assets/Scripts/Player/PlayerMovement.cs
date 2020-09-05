using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10;
    public float rotSpeed = 5;
    [HideInInspector]
    public bool moving;
    [HideInInspector]
    public bool rotating;
    Transform cam;
    BoxCollider boxCollider;
    Rigidbody rb;
    bool grounded;
    bool jumping = false;
    public float gravity = 2;
    Vector3 velocity;
    Vector3 eulerAngleVelocity;
    CharacterController cc;
    float yVelocity = 0;
    public float jumpForce = 2;
    float multiplier = 0;
    public float accelerationSpeed = 2;
    Animator anim;

    private void Start()
    {
        cam = Transform.FindObjectOfType<CameraFollow>().transform;
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        velocity = Vector3.zero;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //movement.x = 0;
        //movement.z = 0;
        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
        {
            if (multiplier < 1)
                multiplier += Time.smoothDeltaTime;
            if (multiplier >= 1)
                multiplier = 1;
        }
        else
        {
            if (multiplier > 0)
                multiplier -= Time.smoothDeltaTime;
            if (multiplier <= 0)
                multiplier = 0;
        }
        Vector3 targetVelocity = Vector3.zero;
        targetVelocity += cam.forward * Input.GetAxis("Vertical");
        targetVelocity += cam.right * Input.GetAxis("Horizontal");
        //movement.Normalize();
        targetVelocity *= speed * Time.smoothDeltaTime;
        velocity = Vector3.Lerp(velocity, targetVelocity, accelerationSpeed * Time.smoothDeltaTime);
        Vector3 aVel = velocity;
        aVel.y = 0;
        anim.SetFloat("MoveSpeed", aVel.magnitude * 5);
        anim.SetBool("Grounded", cc.isGrounded);
        if (!cc.isGrounded)
        {
            yVelocity -= gravity * Time.smoothDeltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
            }
        }
        velocity.y = yVelocity;
    }

    void MoveCharacter(Vector3 direction)
    {
        cc.Move(direction);
        Vector3 vel = cc.velocity;
        vel.y = 0;
        if (vel.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(vel);
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(velocity);
    }
}
