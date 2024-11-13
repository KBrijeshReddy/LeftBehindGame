using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private UpgradableItem sword;
    [SerializeField]
    private float knockback;
    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.gameObject;
            Debug.Log(sword.GetDamage() + " damage dealt to enemy");
            enemy.GetComponent<EnemyHealthManager>().TakeDamage(sword.GetDamage());
            enemy.transform.position += (enemy.transform.position - player.transform.position).normalized * knockback;
            Debug.Log("knockbacked");
            PlayerHealthManager.instance.RegenerateEnergy(sword.GetDamage());
        }
    }
}
