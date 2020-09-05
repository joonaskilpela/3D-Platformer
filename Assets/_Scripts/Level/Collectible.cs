using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    float rotSpeed = 100;
    float hoverSpeed = 5;
    float height = 0.02f;
    FruitCounter counter;

    private void Start()
    {
        counter = FindObjectOfType<FruitCounter>();
        //counter.fruits.Add(this);
        counter.maxCount++;
        counter.CountFruits(0);
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotSpeed * Time.smoothDeltaTime);        
        Vector3 vel = transform.position;
        vel.y = Mathf.Sin(Time.time * hoverSpeed) * height + transform.position.y;
        transform.position = vel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            counter.CountFruits(1);
            GameObject.Destroy(gameObject);
        }
    }
}
