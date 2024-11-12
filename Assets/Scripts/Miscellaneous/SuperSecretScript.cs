using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSecretScript : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            foreach (Item item in items) {
                InventoryManager.instance.Increase(item, 200);
            }
        }
    }
}
