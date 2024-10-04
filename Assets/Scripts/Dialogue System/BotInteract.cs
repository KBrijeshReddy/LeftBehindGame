using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotInteract : MonoBehaviour
{
    [SerializeField]
    private GameObject interactText;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            interactText.SetActive(false);
        }
    }
}
