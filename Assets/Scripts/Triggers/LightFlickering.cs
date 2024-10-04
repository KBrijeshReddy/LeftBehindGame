using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    [SerializeField]
    private Light light;

    async void Start()
    {
        light.intensity = 0;
        await Task.Delay(100);
        light.intensity = 3.14f;
        await Task.Delay(100);
        light.intensity = 0;
        await Task.Delay(200);
        light.intensity = 3.14f;
        await Task.Delay(300);
    }
}
