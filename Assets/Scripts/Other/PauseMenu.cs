using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    private GameObject[] audioButtons;
    private GameObject[] buttons;
    private AudioManager audioManager;
    private GameManager gameManager;


    void Start()
    {
        audioManager = AudioManager.instance;
        gameManager = FindObjectOfType<GameManager>();
        buttons = GameObject.FindGameObjectsWithTag("Button");
        audioButtons = GameObject.FindGameObjectsWithTag("AudioButton");

    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        ButtonSwitch(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioManager.StopUpdateMusicWithFade();
        audioManager.StopMusic();
        audioManager.isMusicWorking = false;
    }

    public void Resume()
    {
        ButtonSwitch(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioManager.isMusicWorking = true;
        gameManager.startMusic = true;
    }

    public void Settings()
    {
        ButtonSwitch(false);
        AudioButtonSwitch(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        AudioButtonSwitch(false);
        ButtonSwitch(true);
    }

    private void ButtonSwitch(bool selector)
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().enabled = selector;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = selector;
        }
    }

    private void AudioButtonSwitch(bool selector)
    {
        foreach (var audioButton in audioButtons)
        {
            audioButton.GetComponent<Image>().enabled = selector;
            audioButton.GetComponentInChildren<TextMeshProUGUI>().enabled = selector;
        }
    }

}
