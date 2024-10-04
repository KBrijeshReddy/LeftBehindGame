using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private UpgradableItem sword;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log(sword.GetDamage() + " damage dealt to enemy");
            other.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(sword.GetDamage());
            PlayerHealthManager.instance.RegenerateEnergy(sword.GetDamage());
        }
    }
}
