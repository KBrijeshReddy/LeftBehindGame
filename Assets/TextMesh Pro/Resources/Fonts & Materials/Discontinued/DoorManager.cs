using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject doorOpenText;

    [SerializeField]
    TMP_Text doorOpenTextChange;

    bool isOpen = false;
    bool trigger = false;

    void Start()
    {
        doorOpenText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) & trigger)
        {
            if (!isOpen)
            {
                anim.Play("doorOpening", 0, 0f);
                isOpen = true;
            }
            else if (isOpen)
            {
                anim.Play("doorClosing", 0, 0f);
                isOpen = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
            doorOpenText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorOpenText.SetActive(true);
        }
    }
}
