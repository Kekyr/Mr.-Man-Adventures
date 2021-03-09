using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    public static bool changeLanguage;
    public static bool fromGame;

    private GameObject name;
    private GameObject[] buttons;
    private GameObject[] settingsButtons;
    private AudioManager audioManager;
    private GameManager gameManager;
    private TextMeshProUGUI text;
    private TextMeshProUGUI nameText;
    private TMP_FontAsset english;
    private Common common;

    public bool startMenu;
    private Vector3 newPosition = new Vector3(10, 650);
    [SerializeField] private List<string> ids = new List<string>();
    [SerializeField] private int currentId;

    private void Start()
    {
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;
        common = FindObjectOfType<Common>();
        english = gameManager.GetComponent<TextLocalizer>().english;
        name = GameObject.FindGameObjectWithTag("Name");
        nameText = name.GetComponent<TextMeshProUGUI>();
        buttons = GameObject.FindGameObjectsWithTag("Button");
        settingsButtons = GameObject.FindGameObjectsWithTag("SettingsButton");
        GameManager.instance.startMusic = true;
        common.ChangeText(buttons, changeLanguage, ids);
        common.ChangeText(settingsButtons, changeLanguage, ids);
    }

    private void Update()
    {
        if (startMenu)
        {
            StartMenu();
        }

        if(changeLanguage)
        {
            ChangeLanguage();
        }
    }

    private void StartMenu()
    {
        name.transform.localPosition = newPosition;


        Common.ButtonSwitch(buttons, true);

        startMenu = false;
    }

    private void ChangeLanguage()
    {
        common.ChangeText(buttons, changeLanguage, ids);

        common.ChangeText(settingsButtons, changeLanguage, ids);

        nameText.font = TextLocalizer.CurrentFont;
        nameText.text = TextLocalizer.ResolveStringValue("menu_settings");

        changeLanguage = false;
    }


    public void StartGame()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 12, false);
        Physics2D.IgnoreLayerCollision(8, 0, false);
        Common.LoadNextScene();

    }

    public void Settings()
    {
        nameText.font = TextLocalizer.CurrentFont;
        nameText.text= TextLocalizer.ResolveStringValue("menu_settings");
        Common.ButtonSwitch(buttons, false);
        Common.SettingsButtonSwitch(settingsButtons, true);
    }

    public void Quit()
    {
        Common.Quit();
    }

    public void Back()
    {
        nameText.font=english;
        name.GetComponent<TextMeshProUGUI>().text = "Mr. Man Adventures";
        Common.SettingsButtonSwitch(settingsButtons, false);
        Common.ButtonSwitch(buttons, true);
    }


}
