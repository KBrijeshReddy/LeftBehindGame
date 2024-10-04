using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GroundSmasher : MonoBehaviour
{
    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject grounds;
    [SerializeField]
    private GameObject groundsl;

    private bool readyTosmash;

    void Start()
    {
        readyTosmash = true;
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyTosmash) {
            GroundSmash();
            Instantiate(grounds, groundsl.transform.position, Quaternion.identity);
            await Task.Delay(5000);
            readyTosmash = true;
        }
    }

    void GroundSmash()
    {
        readyTosmash= false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius); // damage radius is the radius of the area the damage should take place, float var

        // Loop through all hit colliders
        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider has the "Enemy" tag
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damage);
                // Basically damage should be done here
            }
        }
    }

    //Check magicball and ballmagic scripts for refernce
    // ps.magicball should be on player, ballmagic on the Energy ball prefab that will be instantiated
}
