using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public static Dictionary<string, float> volumes;
    public static float mouseSens;


    [Header("-----------------Preset-----------------")]
    [SerializeField]
    private List<GameObject> sound1;
    [SerializeField]
    private List<string> sound1Names;
    [SerializeField]
    private List<GameObject> sound2;
    [SerializeField]
    private List<string> sound2Names;

    public bool firstScene;

    void Awake()
    {
        instance = this;
        firstScene = true;
    }

    public async void ManageMusic() {
        Debug.Log("got ran");
        ReturnMusic("walking").Stop();

        if (firstScene) {
            DontDestroyOnLoad(gameObject);
            await Task.Delay(1500);
            firstScene = false;
            Debug.Log("thing");
        }

        if (SceneNameHolder.scene == "main menu") {
            ReturnMusic("music").Stop();
            ReturnMusic("intense music").Stop();
            ResetSettings();
        } else if (SceneNameHolder.scene == "game end screen" || SceneNameHolder.scene == "death screen") {
            StopPlaying();
            return;
        } else if (SceneNameHolder.scene == "dome") {
            ReturnMusic("music").Stop();
            ReturnMusic("intense music").Play();
        } else if (!ReturnMusic("music").isPlaying)
        {
            ReturnMusic("music").Play();
            ReturnMusic("intense music").Stop();
        }
    }

    public AudioSource ReturnMusic(string name) {
        return sound1[sound1Names.IndexOf(name)].GetComponent<AudioSource>();
    }

    public void PlaySound(string name, Transform pos) {
        Instantiate(sound2[sound2Names.IndexOf(name)], pos.position, Quaternion.identity);
    }

    public void StopPlaying() {
        foreach (GameObject sound in sound1) {
            sound.GetComponent<AudioSource>().Stop();
        }
    }

    public void ResetSettings() {
        volumes = new Dictionary<string, float> {
            {"master", 1f}
        };
        mouseSens = 150f;
    }

    public void ChangeVolume(string name, float value) {
        volumes[name] = value;
        AudioListener.volume = value;
    }
}
