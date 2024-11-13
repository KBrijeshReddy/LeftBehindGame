using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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

    async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("is open", true);
            await Task.Delay(750);
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
