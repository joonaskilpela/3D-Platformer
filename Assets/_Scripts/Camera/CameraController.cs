using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CameraFollow camFollow;
    Transform plyFollower;
    Transform player;
    float sensitivity = 10;
    Vector3 mStartingPos;
    Vector3 mCurPos;
    Camera myCam;
    Transform parent;
    bool moving = false;
    Transform ang;
    Vector3 zBaseAngle;
    Vector3 xBaseAngle;
    float turnAngle;
    float origAngle;
    [HideInInspector]
    public bool respawning = false;

    private float xCamMove
    {
        get
        {
            if (Mathf.Abs(Input.GetAxis("hRightStick")) > 0.3f)
                return Input.GetAxis("hRightStick") * 2;
            else
                return Input.GetAxis("Mouse X");
        }
    }

    private float yCamMove
    {
        get
        {
            if (Mathf.Abs(Input.GetAxis("vRightStick")) > 0.3f)
                return Input.GetAxis("vRightStick") * 2;
            else
                return Input.GetAxis("Mouse Y");
        }
    }

    private void Start()
    {
        camFollow = GetComponent<CameraFollow>();
        plyFollower = FindObjectOfType<PlayerFollower>().transform;
        player = FindObjectOfType<PlayerMovement>().transform;
        myCam = GetComponent<Camera>();
        parent = transform.parent.GetComponent<Transform>();
        ang = FindObjectOfType<AngleChanger>().transform;
    }

    private void Update()
    {
        if (respawning)
            return;
        if (Mathf.Abs(Input.GetAxis("hRightStick")) > 0.5f || Mathf.Abs(Input.GetAxis("vRightStick")) > 0.5f)
        {
            if (!moving)
            {
                zBaseAngle = -ang.forward;
                xBaseAngle = ang.right;
                camFollow.following = false;
                origAngle = transform.eulerAngles.y;
                //mStartingPos = Input.mousePosition;
                //mStartingPos.z = transform.position.z;
                moving = true;
            }
        }
        else
        {
            ang.rotation = player.rotation;
            camFollow.following = true;
            moving = false;
        }
    }

    private void FixedUpdate()
    {
        if (respawning)
            return;

        parent.position = Vector3.Lerp(parent.position, plyFollower.position, 8 * Time.smoothDeltaTime);
        parent.transform.Rotate(parent.up, xCamMove * 2f);
            /*
        if (moving)
        {
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.5f)
            {
                zBaseAngle = transform.forward;
                xBaseAngle = -transform.right;
            }
            Vector3 rot = Vector3.zero;
            rot += xBaseAngle * -Input.GetAxis("hRightStick");
            rot += zBaseAngle * Input.GetAxis("vRightStick");
            //Vector3 rot = new Vector3(parent.right * Input.GetAxisRaw("hRightStick"), 0, myCam.forward * Input.GetAxisRaw("vRightStick"));

            //ang.rotation = Quaternion.Lerp(ang.rotation, Quaternion.Euler(rot), 70 * Time.smoothDeltaTime);

            turnAngle = Mathf.Atan2(Input.GetAxis("hRightStick"), -Input.GetAxis("vRightStick")) * Mathf.Rad2Deg;
            turnAngle += origAngle;
            Vector3 v = ang.rotation.eulerAngles;
            v.y = turnAngle;
            ang.rotation = Quaternion.Lerp(ang.rotation, Quaternion.Euler(v), 7 * Time.smoothDeltaTime);

            parent.position = Vector3.MoveTowards(parent.position, plyFollower.position, 8 * Time.smoothDeltaTime);
            parent.rotation = ang.rotation;
            //ang.rotation = Quaternion.LookRotation(rot);
            //parent.position = Vector3.Lerp(parent.position, plyFollower.position, 7f * Time.smoothDeltaTime);
            //parent.rotation = Quaternion.RotateTowards(parent.rotation, ang.rotation, 70f * Time.smoothDeltaTime);
        }
            */
    }
}
