using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HitButton : MonoBehaviour
{
    [SerializeField]
    private GameObject particleOne;
    [SerializeField]
    public GameObject particleTwo;
    [SerializeField]
    public GameObject parent;
    
    async void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "hammer")
        {  
            GameObject hitOne = Instantiate(particleOne, parent.transform.position, Quaternion.identity);
            GameObject hitTwo = Instantiate(particleTwo, parent.transform.position, parent.transform.rotation);
        }
    }
}
