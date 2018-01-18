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
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Music player: level ");
        _music.Stop();
        switch (scene.buildIndex)
        {
            case 0:
                _music.clip = startClip;
                break;
            case 1:
                _music.clip = gameClip;
                break;
            case 2:
                _music.clip = endClip;
                break;
        }

        _music.loop = transform;
        _music.Play();
    }
}
