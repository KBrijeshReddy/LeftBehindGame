using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyLevelThreeAttack : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    async void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.Play("lvl2_attack");
        }
    }
}
