using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource _music;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            SceneManager.sceneLoaded += SceneLoaded;
            DontDestroyOnLoad(gameObject);
            _music = GetComponent<AudioSource>();
            _music.clip = startClip;
            _music.loop = transform;
            _music.Play();

            Preferences.PreferenceChanged += PlayerPrefsManager_PreferenceChanged;
        }
    }

    private void PlayerPrefsManager_PreferenceChanged(PreferenceChangedEventArgs eventArgs)
    {
        if (eventArgs.PreferenceName == nameof(Preferences.MasterVolume))
        {
            _music.volume = (float)eventArgs.Value;
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Start":
                _music.clip = startClip;
                break;
            case "Game":
                _music.clip = gameClip;
                break;
            case "Lose":
                _music.clip = endClip;
                break;
            default:
                return;
        }

        _music.Stop();
        _music.loop = transform;
        _music.Play();
    }
}
