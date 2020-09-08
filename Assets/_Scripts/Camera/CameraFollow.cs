using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    PlayerMovement plyMovement;
    Transform plyTransform;
    Transform plyFollower;
    public float speed = 2;
    Transform parent;
    float waitTime;
    float timeToWait = 2;
    bool turning = false;
    Quaternion targetRotation;

    private void Start()
    {
        plyMovement = Transform.FindObjectOfType<PlayerMovement>();
        plyTransform = plyMovement.transform;
        plyFollower = Transform.FindObjectOfType<PlayerFollower>().transform;
        parent = transform.parent.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        parent.transform.position = Vector3.Lerp(parent.transform.position, plyFollower.position, speed * Time.smoothDeltaTime);
        if (turning)
        {
            parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, targetRotation, speed * Time.smoothDeltaTime);
            if (Quaternion.Angle(parent.transform.rotation, targetRotation) < 0.01f)
            {
                parent.transform.rotation = plyFollower.rotation;
                turning = false;
            }
        }
        transform.LookAt(plyTransform);
    }

    public void SetWaitTime()
    {
            targetRotation = plyFollower.rotation;
            turning = true;
    }
}
