using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    [SerializeField]
    private Slider sensSlider;

    void Start()
    {
        sensSlider.value = SettingsManager.mouseSens;
    }

    void Update()
    {
        SettingsManager.mouseSens = sensSlider.value;
    }
}
