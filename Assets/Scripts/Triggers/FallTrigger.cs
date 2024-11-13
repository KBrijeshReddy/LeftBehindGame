using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthManager.instance.TakeDamage(1000);
            
        }
    }
}
