// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerTutorial : MonoBehaviour
// {
//     public GameObject sword;
//     public GameObject enemy;

//     void Start()
//     {
//         this.GetComponent<PlayerManager>().enabled = false;
//         hide();
//     }

//     async void Update()
//     {
//         await Task.Delay(6000);
//         this.GetComponent<PlayerManager>().enabled = true;
//         show();
//     }

//     void show()
//     {
//         sword.SetActive(true);
//         enemy.SetActive(true);
//     }
//     void hide()
//     {
//         sword.SetActive(false);
//         enemy.SetActive(false);
//     }
// }
