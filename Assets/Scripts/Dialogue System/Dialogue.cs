using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance; void Awake() { instance = this; }
    
    [HideInInspector]
    public bool isDialougeEnd;
    [HideInInspector]
    public bool canType;


    [SerializeField]
    private DialogueTexts dialogueTexts;

    [Header("-------------Scene-specific-------------")]
    public int domeEnemiesKilled;
    [SerializeField]
    private List<string> endingLines;
    [SerializeField]
    private SceneChanger sceneChanger;
    public bool isTutorial;
    [SerializeField]
    private GameObject tutorialEnemy;

    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private TMP_Text textComp;
    [SerializeField]
    private float textSpeed;


    private int index;
    private List<string> lines = new List<string>();

    void Start()
    {
        domeEnemiesKilled = 0;
        textComp.text = string.Empty;
        canType = false;
        lines.Clear();

        UpdateDialogue(dialogueTexts.GetDialogues());
    }

    void Update()
    {
        if (domeEnemiesKilled == 7)
        UpdateDialogue(endingLines);

        if (Input.GetMouseButtonDown(0) && canType == true) {
            if (textComp.text == lines[index]) {
                NextLine();
            } else
            {
                StopAllCoroutines();
                textComp.text = lines[index];
            }
        }
    }

    public void UpdateDialogue(List<string> dialogues) {
        lines.Clear();
        foreach (string dialogue in dialogues) {
            lines.Add(dialogue);
        }
    }

    public void MoveToNextLine() {
        Debug.Log("hi");
        if (textComp.text == lines[index]) {
            NextLine();
        } else
        {
            StopAllCoroutines();
            textComp.text = lines[index];
        }
    }

    public void StartTyping() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) 
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Count - 1) {
            index++;    
            textComp.text = string.Empty;
            StartCoroutine(TypeLine());
        } else
        {
            dialogueTexts.exhausted = true;
            isDialougeEnd = true;
            canType = false;
            textComp.text = string.Empty;
            UpdateDialogue(dialogueTexts.GetDialogues());
            if (domeEnemiesKilled == 7)
            {
                sceneChanger.gameEnd = true;
            } else if (isTutorial)
            {
                tutorialEnemy.SetActive(true);
            }
        }
    }
}
