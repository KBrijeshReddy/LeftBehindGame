using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "General/Dialogue")]
public class DialogueTexts : ScriptableObject
{
    public List<string> dialogues;
    public List<string> fillers;
    public bool exhausted;

    public List<string> GetDialogues() {
        if (exhausted) {
            return fillers;
        }
        else
        {
            return dialogues;
        }
    }
}
