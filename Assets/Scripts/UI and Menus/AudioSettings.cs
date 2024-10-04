using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;

    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        volumeSlider.value = SettingsManager.volumes["master"];
    }

    void Update()
    {
        SettingsManager.instance.ChangeVolume("master", volumeSlider.value);
    }
}
