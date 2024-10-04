using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject doorOpenText;

    [SerializeField]
    TMP_Text doorOpenTextChange;

    bool isopen = false;
    bool trigger = false;

    void Start()
    {
        doorOpenText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (trigger & !isopen)
            {
                anim.Play("drawerOpening", 0, 0f);
                trigger = false;

                isopen = true;
            }
            if (trigger & isopen)
            {
                anim.Play("drawerClosing", 0, 0f);
                trigger = false;

                isopen = false;
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

            if (isopen)
            {
                //doorOpenTextChange.text= "Close(E)";
                Debug.Log("hi");
            }
        }
    }
}
