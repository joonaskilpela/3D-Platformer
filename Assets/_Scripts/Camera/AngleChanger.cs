using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChanger : MonoBehaviour
{
    Transform player;
    Renderer rnd;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        rnd = GetComponent<Renderer>();
        rnd.enabled = false;
    }

    void Update()
    {
        transform.position = player.position;
    }
}
