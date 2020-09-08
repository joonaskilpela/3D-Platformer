using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinPosition : MonoBehaviour {

    public float minPosition = -100;

	void Start () {
        FindObjectOfType<PlayerMovement>().minPosition = minPosition;
        gameObject.SetActive(false);
	}
}
