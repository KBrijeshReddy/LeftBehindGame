using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.ParticleSystem;

public class MagicBall : MonoBehaviour
{
    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private GameObject parent;


    void Start()
    {
        Destroy(gameObject, lifeTime);
        SplashItUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            SplashItUp();
            Instantiate(particle, parent.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void SplashItUp()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);

        // Loop through all hit colliders
        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider has the "Enemy" tag
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damage);
            }
        }
    }
}
