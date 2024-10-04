using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject Cubics;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Cubics.SetActive(false);
        }
    }
}
