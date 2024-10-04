using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LibraryTrigger : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("wheelchair", 0, 0f);
        }
    }
}
