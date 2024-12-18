using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool pauseMenuOpen;


    [SerializeField]
    private UpgradableItem swordItem;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Canvas settingsPanel;
    [SerializeField]
    private Canvas controlsPanel;

    private bool settingsOpen;
    private bool controlsOpen;
    private List<bool> enemiesActiveBefore = new List<bool>();
    public List<MeleeAttackManager> swordLevels;
    private PlayerManager playerManager;
    private GameObject sword;
    private GameObject enemyContainer;
    public List<GameObject> enemies;

    async void Start()
    {
        pausePanel.SetActive(false);
        pauseMenuOpen = false;
        settingsOpen = false;
        controlsOpen = false;

        playerManager = PlayerManager.instance.gameObject.GetComponent<PlayerManager>();
        sword = GameObject.FindWithTag("SwordMeshChanger");
        swordLevels = new List<MeleeAttackManager>(sword.GetComponentsInChildren<MeleeAttackManager>(true));
        enemyContainer = GameObject.FindWithTag("EnemyContainer");
        for (int i = 0; i < enemyContainer.transform.childCount; i++) {
            enemies.Add(enemyContainer.transform.GetChild(i).gameObject);
        }

        foreach (GameObject enemy in enemies) {
            enemiesActiveBefore.Add(enemy.activeSelf);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (settingsOpen) {
                ToggleSettings();
            } else if (controlsOpen)
            {
                ToggleControls();
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
            Debug.Log("Open pause menu");
        } else
        {
            for (int i = 0; i < enemiesActiveBefore.Count; i++) {
                enemies[i].SetActive(enemiesActiveBefore[i]);
            }

            swordLevels[swordItem.currentLevel].enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Closed pause menu");
        }
    }

    public void ToggleSettings() {
        settingsOpen = !settingsOpen;
        settingsPanel.enabled = settingsOpen;
        pausePanel.SetActive(!settingsOpen);
    }

    public void ToggleControls() {
        controlsOpen = !controlsOpen;
        controlsPanel.enabled = controlsOpen;
        pausePanel.SetActive(!controlsOpen);
    }
}
