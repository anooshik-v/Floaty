using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Button playButton;
    public Button musicButton;
    public Button sfxButton;

    public TextMeshProUGUI musicButtonText;
    public TextMeshProUGUI sfxButtonText;

    private void Start()
    {
        // Add listeners to buttons
        playButton.onClick.AddListener(OnPlayClicked);
        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSFX);
           UpdateUI();
    }

    private void OnPlayClicked()
    {
        // Handle Play button click (for loading a new scene)
        Debug.Log("Play Button Clicked");
        // Replace "GameScene" with the name of your game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();

       UpdateUI();
    }

    private void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
        UpdateUI();

    }

    private void UpdateUI(){
        
        if (AudioManager.instance.GetSFXStatus())
        {
            sfxButtonText.text = "SFX: On";
        }
        else
        {
            sfxButtonText.text = "SFX: Off";
        }


         if (AudioManager.instance.GetMusicStatus())
        {
            musicButtonText.text = "Music: On";
        }
        else
        {
            musicButtonText.text = "Music: Off";
        }
    }
}
