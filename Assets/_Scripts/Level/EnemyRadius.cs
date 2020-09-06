using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour {

    Enemy parent;

    private void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            if (parent == null)
                parent = GetComponentInParent<Enemy>();
            parent.GoAlert();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            if (parent == null)
                parent = GetComponentInParent<Enemy>();
            parent.CalmDown();
        }
    }
}
