using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    private bool isInvCurrentlyActive;

    void Start() {
        isInvCurrentlyActive = false;
    }

    void Update()
    {
        if (isInvCurrentlyActive != InventoryUI.instance.isActive && InventoryManager.instance.nearUpgrader) {
            isInvCurrentlyActive = InventoryUI.instance.isActive;
            text.SetActive(!isInvCurrentlyActive);
        }
    }

    public void SetTextActive(bool active) {
        text.SetActive(active);
    }

    async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(true);
        }
    }

    async void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(false);
        }
    }
}
