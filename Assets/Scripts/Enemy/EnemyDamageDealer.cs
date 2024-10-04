using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float cooldown;
    private float elapsed;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            PlayerHealthManager.instance.TakeDamage(damage);
            elapsed = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            elapsed += Time.deltaTime;
            if (elapsed >= cooldown) {
                elapsed = 0f;
                PlayerHealthManager.instance.TakeDamage(damage);
            }
        }
    }
}
