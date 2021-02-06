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
    private bool settingsIsClosed=true;


    void Start()
    {
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;
        buttons = GameObject.FindGameObjectsWithTag("Button");
        audioButtons = GameObject.FindGameObjectsWithTag("AudioButton");
    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && settingsIsClosed)
        {
            if (gameIsPaused)
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
        Common.ButtonSwitch(buttons,true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioManager.StopUpdateMusicWithFade();
        audioManager.StopMusic();
    }

    public void Resume()
    {
        Common.ButtonSwitch(buttons,false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameManager.startMusic = true;
    }

    public void Settings()
    {
        settingsIsClosed = false;
        Common.ButtonSwitch(buttons,false);
        Common.AudioButtonSwitch(audioButtons, true);
    }

    public void Quit()
    {
        Common.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        Common.AudioButtonSwitch(audioButtons,false);
        Common.ButtonSwitch(buttons,true);
        settingsIsClosed = true;
    }

    

    

}
