using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    private static MenuMusicController instance = null;
    public AudioSource _audioSource;
    
    public static MenuMusicController Instance => instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}