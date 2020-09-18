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
    public List<AudioClip> idleSounds = new List<AudioClip>();
    public List<AudioClip> alertSounds = new List<AudioClip>();
    public List<AudioClip> hitSounds = new List<AudioClip>();
    public AudioClip deathSound;
    AudioSource auSource;
    AudioClip tempClip;
    bool alert = false;
    float soundTimer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        plyTransform = player.transform;
        auSource = GetComponent<AudioSource>();
        NextSound();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            transform.rotation = Quaternion.identity;
        else
        {
            if (alert)
            {
                transform.LookAt(plyTransform);
            }
            else if(soundTimer < Time.time)
            {
                tempClip = idleSounds[Random.Range(0, idleSounds.Count - 1)];
                auSource.clip = tempClip;
                auSource.Play();
                NextSound();
            }
        }
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
                        auSource.PlayOneShot(deathSound);
                        GetComponent<BoxCollider>().enabled = false;
                        GetComponent<SphereCollider>().enabled = true;
                        other.GetComponent<PlayerMovement>().yVelocity = other.GetComponent<PlayerMovement>().jumpForce;
                    }
                    else
                    {
                        //Vector3 dir = other.transform.position - transform.position;
                        //other.GetComponent<CharacterController>().Move(dir * knockBack);
                        tempClip = hitSounds[Random.Range(0, hitSounds.Count - 1)];
                        auSource.PlayOneShot(tempClip);
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

    public void GoAlert()
    {
        alert = true;
        anim.SetBool("SeeingSomething", true);
        tempClip = alertSounds[Random.Range(0, alertSounds.Count - 1)];
        auSource.clip = tempClip;
        auSource.Play();
        transform.LookAt(plyTransform.position);
    }

    public void CalmDown()
    {
        alert = false;
        anim.SetBool("SeeingSomething", false);
    }

    void NextSound()
    {
        soundTimer = Time.time + Random.Range(2f, 8f);
    }
}
