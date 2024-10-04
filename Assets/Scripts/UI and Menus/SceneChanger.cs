using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool gameEnd;

    [SerializeField]
    private int sceneToEnter;
    [SerializeField]
    private int domeScene;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("real");
        if (other.gameObject.tag == "Player")
        {
            if (gameEnd) { 
                SceneManager.LoadScene(domeScene);
            } else
            {
                SceneManager.LoadScene(sceneToEnter);
            }
        }
    }
}
