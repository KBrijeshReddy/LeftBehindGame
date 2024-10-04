using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationTriggerManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    public  Animator anim2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("wheelchair", 0, 0f);
            anim2.Play("paintingfalling", 0, 0f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
