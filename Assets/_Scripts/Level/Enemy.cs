using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EnemyType { blob, spiky };
    public EnemyType myType;
    Animator anim;
    float knockBack = 1f;
    PlayerMovement player;
    Transform plyTransform;
    float distance;
    Animation ama;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        plyTransform = player.transform;
        ama = GetComponent<Animation>();
    }

    private void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            distance = Vector3.Distance(transform.position, plyTransform.position);
            if (distance < 10)
            {
                anim.SetBool("SeeingSomething", true);
                transform.LookAt(plyTransform.position);
            }
            else
            {
                anim.SetBool("SeeingSomething", false);
            }
        }
        else
            transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            if (other.GetComponent<PlayerMovement>())
            {
                if (myType == EnemyType.blob)
                {
                    if (other.transform.position.y > transform.position.y)
                    {
                        anim.SetBool("Dying", true);
                        GetComponent<BoxCollider>().enabled = false;
                        GetComponent<SphereCollider>().enabled = true;
                        other.GetComponent<PlayerMovement>().yVelocity = other.GetComponent<PlayerMovement>().jumpForce;
                    }
                    else
                    {
                        //Vector3 dir = other.transform.position - transform.position;
                        //other.GetComponent<CharacterController>().Move(dir * knockBack);
                        other.GetComponent<PlayerMovement>().knockBackForce = knockBack;
                        other.GetComponent<PlayerMovement>().pushingBack = true;
                    }
                }
                else if (myType == EnemyType.spiky)
                {
                    other.GetComponent<PlayerMovement>().knockBackForce = knockBack;
                    other.GetComponent<PlayerMovement>().pushingBack = true;
                }
            }
        }
    }
}
