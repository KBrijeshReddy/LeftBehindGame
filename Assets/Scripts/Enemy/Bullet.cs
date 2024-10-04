using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float damage;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other) {
        // Debug.Log("Instantiate Particles");

        // Check if it hit the player or other objects and handle accordingly
        if (other.gameObject.CompareTag("Player"))
        {
            // Handle player hit logic here
            // Debug.Log("Player hit!");
            PlayerHealthManager.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
