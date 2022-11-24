using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    ////////// PUBLIC FIELDS //////////
    public GameObject shrapnelEffect;    // Particle effect that simulates shrapnel

    float bulletSpeed;
    float bulletDuration;

    ////////// PRIVATE FIELDS //////////
    GameObject player;
    PlayerController playerController;
    Rigidbody rb;
 
    void Start()
    {
        ////////// INITIALIZATIONS //////////
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        bulletSpeed = playerController.bulletSpeed;
        bulletDuration = playerController.bulletDuration;

        Destroy(gameObject, bulletDuration);
    }

    void FixedUpdate()
    {
        // Moves the bullet forward.
        rb.MovePosition(transform.position + (transform.forward * bulletSpeed));
    }

    // Instantiates shrapnel and destroys the bullet.
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(shrapnelEffect,transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
