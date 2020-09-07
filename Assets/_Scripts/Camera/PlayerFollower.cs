using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public bool following = true;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
