using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyLevelOneAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attack;
    [SerializeField]
    private float InstantiationTimer;
    [SerializeField]
    private GameObject parent;

    async void Update()
    {
        InstantiationTimer -= Time.deltaTime;

        if (InstantiationTimer <= 0)
        {
            GameObject at =  Instantiate(attack, parent.transform.position, Quaternion.identity);
            InstantiationTimer = 1f;
            await Task.Delay(100);
            Destroy(at);
        }
    }
}
