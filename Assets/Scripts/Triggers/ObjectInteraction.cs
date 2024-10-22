using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField]
    protected string objectName;
    [SerializeField]
    protected GameObject text;
    [SerializeField]
    protected bool isInvCurrentlyActive;

    void Start()
    {
        isInvCurrentlyActive = false;
    }

    void Update()
    {
        if (isInvCurrentlyActive != InventoryUI.isActive && InventoryManager.nearInteractors[objectName]) {
            isInvCurrentlyActive = InventoryUI.isActive;
            text.SetActive(!isInvCurrentlyActive);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            PreInteraction(true);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.CompareTag("Player")) {
            PreInteraction(false);
        }
    }

    protected virtual void PreInteraction(bool active) {
        InventoryManager.nearInteractors[objectName] = active;
        text.SetActive(active);
    }
}
