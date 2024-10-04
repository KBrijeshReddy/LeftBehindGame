using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject thing;
    
    void Start()
    {
        thing.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            thing.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            thing.SetActive(false);
        }
    }
}
