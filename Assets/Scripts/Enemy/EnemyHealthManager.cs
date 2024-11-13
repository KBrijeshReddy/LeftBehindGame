using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    [HideInInspector]
    public float health;


    [SerializeField]
    private string deathSFX;
    [SerializeField]
    private string damageSFX;
    [SerializeField]
    private EnemyLoot loot;
    [SerializeField]
    private GameObject objectToDestroy;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private float maxHealth;

    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    private Image healthBar;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, health/maxHealth, lerpSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Enemy got hit with damage " + damage);

        if (health <= 0) {
            Instantiate(particle, parent.transform.position, Quaternion.identity);
            SettingsManager.instance.PlaySound(deathSFX, transform);

            for (int i = 0; i < loot.items.Count; i++) {
                InventoryManager.instance.Increase(loot.items[i], loot.counts[i]);
            }

            if (SceneNameHolder.scene == "dome")
            Dialogue.instance.domeEnemiesKilled += 1;

            Destroy(gameObject);
        } else
        {
            SettingsManager.instance.PlaySound(damageSFX, transform);
        }
    }
}
