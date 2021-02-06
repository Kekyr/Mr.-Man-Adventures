using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject name;
    private GameObject[] buttons;
    private GameObject[] audioButtons;
    private AudioManager audioManager;
    private GameManager gameManager;

    public bool startMenu;
    private Vector3 newPosition = new Vector3(10, 650);

    private void Start()
    {
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;
        name = GameObject.FindGameObjectWithTag("Name");
        buttons = GameObject.FindGameObjectsWithTag("Button");
        audioButtons = GameObject.FindGameObjectsWithTag("AudioButton");

    }

    private void Update()
    {
        if (startMenu)
        {
            name.transform.localPosition = newPosition;

            Common.ButtonSwitch(buttons, true);

            startMenu = false;
        }
    }

    public void StartGame()
    {
        Common.LoadNextScene();
    }

    public void Settings()
    {
        name.GetComponent<TextMeshProUGUI>().text = "Settings";
        Common.ButtonSwitch(buttons, false);
        Common.AudioButtonSwitch(audioButtons, true);
    }

    public void Quit()
    {
        Common.Quit();
    }

    public void Back()
    {
        name.GetComponent<TextMeshProUGUI>().text = "Mr. Man Adventures";
        Common.AudioButtonSwitch(audioButtons, false);
        Common.ButtonSwitch(buttons, true);
        gameManager.startMusic = true;
    }


}
