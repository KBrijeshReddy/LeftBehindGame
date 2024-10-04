using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private int tutorialSceneNumber;
    [SerializeField]
    private int creditsSceneNumber;
    [SerializeField]
    private int mainMenuSceneNumber;
    [SerializeField]
    private int checkPointSceneNumber;


    public void PlayGame()
    {
        SceneManager.LoadScene(tutorialSceneNumber);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsSceneNumber);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneNumber);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(checkPointSceneNumber);
    }

    public void QuitGame()
    {
        Debug.Log("quitted");
        Application.Quit();
    }
}
