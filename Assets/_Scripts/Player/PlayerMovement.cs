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
    [HideInInspector]
    public float yVelocity = 0;
    public float jumpForce = 2;
    float multiplier = 0;
    public float accelerationSpeed = 2;
    Animator anim;
    [HideInInspector]
    public bool pushingBack = false;
    [HideInInspector]
    public float knockBackForce = 1;
    [HideInInspector]
    public float minPosition = -1000;
    Vector3 startPosition;
    bool mouseUsed = false;
    float inputTimer;
    float nextInputTime = 1;
    bool respawning = false;
    CameraController camControl;

    private void Start()
    {
        cam = Transform.FindObjectOfType<CameraFollow>().transform;
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        velocity = Vector3.zero;
        anim = GetComponentInChildren<Animator>();
        startPosition = transform.position;
        camControl = FindObjectOfType<CameraController>();
        if (!mouseUsed)
            Cursor.visible = false;

    }    

    private void Update()
    {
        //movement.x = 0;
        //movement.z = 0;
        if (respawning)
        {
            return;
        }
        if(inputTimer < Time.time)
        {
            cam.GetComponent<CameraFollow>().SetWaitTime();
            inputTimer = Time.time + nextInputTime;
        }
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
        targetVelocity *= speed;
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
        if(transform.position.y < minPosition)
        {
            Respawn();
        }
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
        if (respawning)
            return;
        MoveCharacter(velocity);
        if (pushingBack)
        {
            PushBack(knockBackForce);
        }
    }

    public void PushBack(float force)
    {
        Vector3 dir = transform.forward * -force * Time.smoothDeltaTime;
        dir.z = -1;
        dir.y += 0.1f;
        //dir.Normalize();
        cc.Move(dir);
        //transform.Translate(dir);
        if(force > 0.5f)
        {
            knockBackForce *= 0.8f;
        }
        else
        {
            pushingBack = false;
        }
    }

    void Respawn()
    {
        respawning = true;
        cam.GetComponent<CameraFollow>().respawning = true;
        camControl.respawning = true;
        transform.position = startPosition;
    }
    public void StartGame()
    {
        respawning = false;
        camControl.respawning = false;
    }
}
