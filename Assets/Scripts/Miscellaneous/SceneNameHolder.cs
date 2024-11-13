using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameHolder : MonoBehaviour
{
    public static string scene;

    [SerializeField]
    private string likeActuallyGuessWhatSceneThisIs;

    void Awake()
    {
        scene = likeActuallyGuessWhatSceneThisIs;
    }

    void Start()
    {
        SettingsManager.instance.ManageMusic();
    }
}
