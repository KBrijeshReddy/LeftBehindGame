using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] meshes;
    [SerializeField]
    private UpgradableItem item;
   
    void Start()
    {
        UpdateMesh();
    }

    public void UpdateMesh() {
        for (int i = 0; i < meshes.Length; i++) {
            meshes[i].SetActive(item.currentLevel == i);
        }
    }
}
