using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance; void Awake() { instance = this; }
    public static float energy;
    public static float health;

    
    // public PostProcessVolume pp;
    // public Vignette vig;


    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private Animator deathAnim;
    [SerializeField]
    private Animator blackoutAnim;
    [SerializeField]
    private int deathScreen;
    [SerializeField]
    private int tutorialScene;
    [SerializeField]
    private Image energyBar;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float healAmount;
    [SerializeField]
    private float energyDecreaseRate;
    [SerializeField]
    private float energyIncreaseProportion;
    [SerializeField]
    private float lerpSpeed;


    void Start()
    {
        maxHealth = 100f;
        energy = maxHealth;
        health = maxHealth;
        // pp.profile.TryGetSettings(out vig);
    }

    void Update()
    {
        if (energy <= 0) {
            Debug.Log("Game over(energy became 0)");
        } else
        {
            energy -= energyDecreaseRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        ConsumeEnergy();

        energyBar.fillAmount = Mathf.MoveTowards(energyBar.fillAmount, energy/maxHealth, lerpSpeed * Time.deltaTime);
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, health/maxHealth, lerpSpeed * Time.deltaTime);
        healthText.text = health.ToString();
    }

    async public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            health= 0; 
            Cursor.lockState = CursorLockMode.None;
            SettingsManager.instance.StopPlaying();
            // deathAnim.Play("death");

            await Task.Delay(500);
            blackoutAnim.Play("blackout");
            await Task.Delay(1500);

            if (Dialogue.instance.isTutorial) {
                SceneManager.LoadScene(tutorialScene);
            } else
            {
                SceneManager.LoadScene(deathScreen);
            }

            Cursor.visible = true;
        } else if(health > maxHealth)
        {
            health = maxHealth;
        }


        // if (health <= 30)
        // {
        //     vig.intensity.value = 0.528f;
        //     vig.color.value = Color.red;
        // } else
        // {
        //     vig.intensity.value = 0.114f;
        // }

        PlayerManager.instance.inCombat = true;
        await Task.Delay(10000);
        PlayerManager.instance.inCombat = false;
    }

    public void RegenerateEnergy(float damage) {
        energy += damage * energyIncreaseProportion;

        if (energy > maxHealth)
        {
            energy = maxHealth;
        }
    }

    public void ConsumeEnergy() {
        if (energy < healAmount) {
            Debug.Log("Can't consume energy");
            return;
        }

        energy -= healAmount;
        TakeDamage(-healAmount);
    }
}
