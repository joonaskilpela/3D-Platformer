using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVisibility : MonoBehaviour {

    Renderer rnd;
    Shader ogShader;
    Shader transparent;

    private void Start()
    {
        rnd = GetComponent<Renderer>();
        ogShader = rnd.material.shader;
        transparent = Shader.Find("Transparent/Diffuse");
        Color col = rnd.material.color;
        col.a = 0.3f;
        rnd.material.color = col;
    }

    public void MakeVisible()
    {
        rnd.material.shader = ogShader;
    }

    public void FadeAway()
    {
        rnd.material.shader = transparent;
    }
}
