using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    PlayerMovement plyMovement;
    Transform plyTransform;
    Transform plyFollower;
    public float speed = 2;
    Transform parent;

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
        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, plyFollower.transform.rotation, speed * Time.smoothDeltaTime);
        transform.LookAt(plyTransform);
    }
}
