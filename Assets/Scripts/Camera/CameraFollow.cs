using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform player;
    public float speed = 200;

    private void Start()
    {
        player = Transform.FindObjectOfType<PlayerMovement>().transform;
    }

    private void LateUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 forward = player.transform.forward * speed;
        Vector3 needPos = player.transform.position - forward;

        transform.position = Vector3.SmoothDamp(transform.position, needPos, ref velocity, 0.5f);
        //transform.rotation = player.rotation;
        transform.LookAt(player);
    }
}
