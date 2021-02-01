using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioClip[] audioClips;
    private GameObject name;
    private GameObject[] buttons;
    private GameObject[] audioButtons;
    private AudioManager audioManager;

    public bool startMenu;
    public bool startMusic;
    private Vector3 newPosition = new Vector3(10, 650);
    private int clipNumber;

    private void Start()
    {
        audioManager = AudioManager.instance;
        name = GameObject.FindGameObjectWithTag("Name");
        buttons = GameObject.FindGameObjectsWithTag("Button");
        audioButtons = GameObject.FindGameObjectsWithTag("AudioButton");
        clipNumber = Random.Range(0, audioClips.Length);
        startMusic = true;
    }

    private void Update()
    {
        if (startMenu)
        {
            name.transform.localPosition = newPosition;

            ButtonSwitch(true);

            startMenu = false;
        }

        if (startMusic)
        {
            audioManager.StartMusic(clipNumber, audioClips);
            startMusic = false;
        }
    }

    public void StartGame()
    {
        Common.LoadNextScene();
    }

    public void Settings()
    {
        name.GetComponent<TextMeshProUGUI>().text = "Settings";
        ButtonSwitch(false);
        AudioButtonSwitch(true);
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


    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        name.GetComponent<TextMeshProUGUI>().text = "Mr. Man Adventures";
        AudioButtonSwitch(false);
        ButtonSwitch(true);
    }


}
