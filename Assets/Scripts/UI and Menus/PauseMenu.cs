using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private UpgradableItem swordItem;
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Canvas settingsPanel;

    private bool pauseMenuOpen;
    private bool settingsOpen;
    private List<bool> enemiesActiveBefore = new List<bool>();
    private MeleeAttackManager[] swordLevels;
    private PlayerManager playerManager;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseMenuOpen = false;
        settingsOpen = false;
        playerManager = PlayerManager.instance.gameObject.GetComponent<PlayerManager>();
        swordLevels = sword.GetComponentsInChildren<MeleeAttackManager>();
        
        foreach (var enemy in enemies) {
            enemiesActiveBefore.Add(enemy.activeSelf);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (settingsOpen) {
                ToggleSettings();
            } else
            {
                TogglePauseMenu();
            }
        }
    }

    public void TogglePauseMenu() {
        pauseMenuOpen = !pauseMenuOpen;

        Cursor.visible = pauseMenuOpen;
        pausePanel.SetActive(pauseMenuOpen);
        playerManager.enabled = !pauseMenuOpen;

        if (pauseMenuOpen) {
            for (int i = 0; i < enemiesActiveBefore.Count; i++) {
                enemiesActiveBefore[i] = enemies[i].activeSelf;
                enemies[i].SetActive(false);
            }

            swordLevels[swordItem.currentLevel].enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Closed pause menu");
        } else
        {
            for (int i = 0; i < enemiesActiveBefore.Count; i++) {
                enemies[i].SetActive(enemiesActiveBefore[i]);
            }

            swordLevels[swordItem.currentLevel].enabled = true;;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Opened pause menu");
        }
    }

    public void ToggleSettings() {
        settingsOpen = !settingsOpen;
        settingsPanel.enabled = settingsOpen;
        pausePanel.SetActive(!settingsOpen);
    }
}
