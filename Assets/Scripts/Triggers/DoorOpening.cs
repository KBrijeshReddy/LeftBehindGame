using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private List<GameObject> enemies;

    void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("is open", true);
            foreach (var thing in enemies) {
                thing.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("is open", false);
        }
    }
}
