using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        transform.parent = FindObjectOfType<CameraFollow>().transform;
        transform.position = transform.parent.position;
    }
}
