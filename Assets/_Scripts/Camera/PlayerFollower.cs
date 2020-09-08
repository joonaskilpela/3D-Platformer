using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public bool following = true;
    Renderer rnd;
    Shader ogShader;

    private void Start()
    {
        rnd = GetComponent<Renderer>();
        rnd.enabled = false;
    }
}
