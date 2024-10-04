using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Canvas settingsPanel;

    private bool pauseMenuOpen;
    private PlayerManager playerManager;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseMenuOpen = false;
        playerManager = PlayerManager.instance.gameObject.GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsPanel.enabled) {
            if (pauseMenuOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Closed pause menu");
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Opened pause menu");
            }
            Cursor.visible = !pauseMenuOpen;
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenuOpen = !pauseMenuOpen;

        pausePanel.SetActive(pauseMenuOpen);
        playerManager.enabled = !pauseMenuOpen;
        playerManager.meleeAttackManagers.SetActive(!pauseMenuOpen);

        foreach (var thing in enemies)
        {
            thing.SetActive(!pauseMenuOpen);
        }
    }

    public void ToggleSettings()
    {
        TogglePauseMenu();
        settingsPanel.enabled = !settingsPanel.enabled;
    }
}
