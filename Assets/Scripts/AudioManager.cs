using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource; 

    private bool isMusicOn = true;
    private bool isSfxOn = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        musicSource= GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        sfxSource= GameObject.Find("SFXSource").GetComponent<AudioSource>();

        // Load saved settings from PlayerPrefs
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isSfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        if (musicSource != null)
        {
            if (isMusicOn)
                musicSource.Play();
            else
                musicSource.Pause();
        }

        sfxSource.mute = !isSfxOn;
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        if (musicSource != null)
        {
            if (isMusicOn)
                musicSource.Play();
            else
                musicSource.Pause();
        }
    }

    public void ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        PlayerPrefs.SetInt("SFXOn", isSfxOn ? 1 : 0);
       
        sfxSource.mute = !isSfxOn;
        
    }

    public bool GetMusicStatus()
    {
        return isMusicOn;
    }

    public bool GetSFXStatus()
    {
        return isSfxOn;
    }
}
