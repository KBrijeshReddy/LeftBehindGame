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
    private UpgradableItem robot;
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
    private float lerpSpeed;


    [Header("-----------------Values-----------------")]
    [SerializeField]
    private List<string> abilities;
    [SerializeField]
    private List<float> energyRequired;
    [SerializeField]
    private float healthHealed;
    [SerializeField]
    private float energyIncreaseProportion;


    private float energyIncreaseRate;


    void Start()
    {
        maxHealth = 100f;
        energy = maxHealth;
        health = maxHealth;
        energyIncreaseRate = robot.GetDamage();
        // pp.profile.TryGetSettings(out vig);
    }

    void Update()
    {
        if (energy <= 100) {
            energy += energyIncreaseRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && health < maxHealth) {
            if (UseEnergy("heal"))
            TakeDamage(-healthHealed);
        }

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

    public bool UseEnergy(string ability) {
        float amount = energyRequired[abilities.IndexOf(ability)];
        if (energy < amount) {
            Debug.Log("Can't use energy");
            return false;
        }
        energy -= amount;
        return true;
    }
}
