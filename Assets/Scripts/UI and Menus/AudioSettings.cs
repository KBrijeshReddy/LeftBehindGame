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
        volumeSlider.value = SettingsManager.volumes["master"];
    }

    void Update()
    {
        SettingsManager.instance.ChangeVolume("master", volumeSlider.value);
    }
}
