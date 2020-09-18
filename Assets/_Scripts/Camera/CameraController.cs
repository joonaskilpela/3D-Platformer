using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CameraFollow camFollow;
    Transform plyFollower;
    Transform player;
    Vector3 mStartingPos;
    Vector3 mCurPos;
    Transform parent;
    bool moving = false;
    Transform ang;
    float turnAngle;
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
                camFollow.following = false;
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
    }
}
