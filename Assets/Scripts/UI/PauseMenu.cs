using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool changeLanguage;

    private GameObject[] settingsButtons;
    [SerializeField] private GameObject[] buttons;
    private AudioManager audioManager;
    private GameManager gameManager;
    private TextMeshProUGUI text;
    private Common common;
    private PlayerMovement playerMovement;
    
    [SerializeField] private List<string> ids = new List<string>();
    [SerializeField] private int currentId;
    private string id;
    private bool settingsIsClosed = true;


    private void Start()
    {
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;
        common = FindObjectOfType<Common>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        buttons = GameObject.FindGameObjectsWithTag("Button");
        settingsButtons = GameObject.FindGameObjectsWithTag("SettingsButton");

        common.ChangeText(buttons,changeLanguage,ids);
        common.ChangeText(settingsButtons, changeLanguage, ids);
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
        else if(Input.GetKeyDown(KeyCode.Escape) && !settingsIsClosed)
        {
            Back();
        }

        if (Input.GetButtonDown("Restart"))
        {
            RestartLevel();
        }

        if (changeLanguage)
        {
            ChangeLanguage();
        }
}
    private void ChangeLanguage()
    {
        common.ChangeText(buttons, changeLanguage, ids);

        common.ChangeText(settingsButtons, changeLanguage, ids);

        changeLanguage = false;
    }


    public void Pause()
    {
        Common.ButtonSwitch(buttons,true);
        Time.timeScale = 0f;
        playerMovement.enabled = false;
        gameIsPaused = true;
    }

    public void Resume()
    {
        Common.ButtonSwitch(buttons,false);
        Time.timeScale = 1f;
        playerMovement.enabled = true;
        gameIsPaused = false;
    }

    public void Settings()
    {
        settingsIsClosed = false;
        Common.ButtonSwitch(buttons,false);
        Common.SettingsButtonSwitch(settingsButtons, true);
    }

    public void Quit()
    {
        Common.Quit();
    }

    public void MainMenu()
    {
        Common.MainMenu();
    }

    public void Back()
    {
        Common.SettingsButtonSwitch(settingsButtons,false);
        Common.ButtonSwitch(buttons,true);
        settingsIsClosed = true;
    }

    public void RestartLevel()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 12, false);
        Physics2D.IgnoreLayerCollision(8, 0, false);
        LoadCurrentScene();
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    

    

}
