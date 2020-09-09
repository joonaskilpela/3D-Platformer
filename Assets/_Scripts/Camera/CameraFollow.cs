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
    Vector3 boxDimensions = new Vector3(2, 2, 2);
    [HideInInspector]
    public bool following = true;
    [HideInInspector]
    public bool respawning = false;

    private void Start()
    {
        plyMovement = Transform.FindObjectOfType<PlayerMovement>();
        plyTransform = plyMovement.transform;
        plyFollower = Transform.FindObjectOfType<PlayerFollower>().transform;
        parent = transform.parent.GetComponent<Transform>();
    }

    private void Update()
    {
        //if (hits != null)
        //{
        //    foreach (RaycastHit hit in hits)
        //    {
        //        Renderer r = hit.collider.GetComponent<Renderer>();
        //        ChangeVisibility c = hit.collider.GetComponent<ChangeVisibility>();
        //        if (c)
        //        {
        //            c.MakeVisible();
        //        }
        //    }
        //}

        //hits = Physics.RaycastAll(transform.position, (plyTransform.position - transform.position),
        //    Vector3.Distance(transform.position, plyTransform.position));
        //hits = Physics.BoxCastAll(transform.position, boxDimensions, transform.forward, transform.rotation, (Vector3.Distance(plyTransform.position, transform.position) * 0.7f));
        //foreach (RaycastHit h in hits)
        //{
        //    //Renderer r = h.collider.GetComponent<Renderer>();
        //    ChangeVisibility c = h.collider.GetComponent<ChangeVisibility>();
        //    if (c)
        //    {
        //        if (Vector3.Distance(h.point, transform.position) < 5)
        //        {
        //            c.FadeAway();
        //            //r.enabled = false;
        //        }
        //    }
        //}
    }

    private void FixedUpdate()
    {
        if (respawning)
        {
            Vector3 velocity = Vector3.zero;
            parent.transform.position = Vector3.Lerp(parent.transform.position, plyFollower.position, speed * 3 * Time.smoothDeltaTime);
            if (turning)
            {
                parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, targetRotation, speed * 3 * Time.smoothDeltaTime);
                if (Quaternion.Angle(parent.transform.rotation, targetRotation) < 0.01f)
                {
                    parent.transform.rotation = plyFollower.rotation;
                    turning = false;
                }
            }
            if(Vector3.Distance(parent.transform.position, plyFollower.position) < 0.1f)
            {
                respawning = false;
                plyMovement.StartGame();
            }
        }
        else if (following)
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
        }
        transform.LookAt(plyTransform);
    }

    public void SetWaitTime()
    {
        targetRotation = plyFollower.rotation;
        turning = true;
    }
}
