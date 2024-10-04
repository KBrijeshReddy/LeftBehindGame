using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : ObjectInteraction
{
    [SerializeField]
    private int chestID;

    protected override void PreInteraction(bool active) {
        InventoryManager.nearInteractors[objectName] = active;
        text.SetActive( InventoryManager.instance.ChangeChestID(chestID, active) );
    }
}
