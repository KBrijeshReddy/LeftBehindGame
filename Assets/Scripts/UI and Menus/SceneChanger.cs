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
    private int endScreen;
    [SerializeField]
    private bool isDomeGate;
    [SerializeField]
    private UpgradableItem robot;
    [SerializeField]
    private GameObject text;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameEnd) { 
                SceneManager.LoadScene(endScreen);
            } else if (!isDomeGate)
            {
                SceneManager.LoadScene(sceneToEnter);
            } else if (robot.InFinalLevel())
            {
                SceneManager.LoadScene(sceneToEnter);
            } else
            {
                text.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(false);
        }
    }
}
