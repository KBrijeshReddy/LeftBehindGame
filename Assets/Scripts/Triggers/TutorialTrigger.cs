using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private List<string> finalDialogues;
    [SerializeField]
    private NavAI petNavAI;

    void Start() {
        petNavAI.enabled = false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            enemy.SetActive(true);
            petNavAI.enabled = true;
            Dialogue.instance.UpdateDialogue(finalDialogues);
        }
    }
}
