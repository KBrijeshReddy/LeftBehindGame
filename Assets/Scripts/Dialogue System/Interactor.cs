using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private float interactRange;

    private bool isDialougeRunning = false;
    private bool rand = false;
    private Dialogue dialouge;
    private Animator animator;

    void Start()
    {
        dialouge = Dialogue.instance;
        animator = dialouge.gameObject.GetComponent<Animator>();
        // Interact.SetActive(false);
    }

    void Update()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position,  interactRange);

        if (Input.GetKeyDown(KeyCode.R)) {            
            foreach (Collider collider in colliderArray) {
                if (collider.TryGetComponent(out BotInteract botInteract) && animator.GetBool("isOpen") == false) {
                    dialouge.canType = true;
                    StartDialouge();
                }
            }
        }
        
        if (dialouge.isDialougeEnd == true) {
            animator.SetBool("isOpen", false);  
            dialouge.isDialougeEnd =  false;
        }
    }

    void StartDialouge() {
        animator.SetBool("isOpen",  true);
        dialouge.isDialougeEnd = false;
        dialouge.StartTyping();
    }
}
