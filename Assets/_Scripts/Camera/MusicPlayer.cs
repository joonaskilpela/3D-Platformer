using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Play music at camera's position
// This exists because changing and applying the camera prefab also changes the song for every level
public class MusicPlayer : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        transform.parent = FindObjectOfType<CameraFollow>().transform;
        transform.position = transform.parent.position;
    }
}
