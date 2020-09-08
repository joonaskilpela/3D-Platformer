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
    RaycastHit[] hits = null;

    private void Start()
    {
        plyMovement = Transform.FindObjectOfType<PlayerMovement>();
        plyTransform = plyMovement.transform;
        plyFollower = Transform.FindObjectOfType<PlayerFollower>().transform;
        parent = transform.parent.GetComponent<Transform>();
    }

    private void Update()
    {
        if(hits != null)
        {
            foreach(RaycastHit hit in hits)
            {
                Renderer r = hit.collider.GetComponent<Renderer>();
                if (r)
                {
                    r.enabled = true;
                }
            }
        }
        hits = Physics.RaycastAll(transform.position, (plyTransform.position - transform.position),
            Vector3.Distance(transform.position, plyTransform.position));
        foreach(RaycastHit h in hits)
        {
            Renderer r = h.collider.GetComponent<Renderer>();
            if (r)
            {
                r.enabled = false;
            }
        }
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
