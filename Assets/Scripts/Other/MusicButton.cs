using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public static bool musicIsOff;

    public Sprite on;
    public Sprite off;
    private Image image;
    private AudioManager audioManager;
    private bool isPaused;


    private void Start()
    {
       
        audioManager = AudioManager.instance;
        image = GetComponent<Image>();
        isPaused = true;


        if (PlayerPrefs.GetInt("Music") != 0)
        {
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                musicIsOff = true;
                audioManager.isMusicWorking = false;
                isPaused = false;
            }
            else
            {
                musicIsOff = false;
                audioManager.isMusicWorking = true;
            }
        }

        if (musicIsOff)
        {
            image.sprite = off;
        }
        else
        {
            image.sprite = on;
        }
    }

    public void MusicSwitch()
    {
        if (musicIsOff)
        {
            image.sprite = on;
            musicIsOff = false;
            audioManager.isMusicWorking = true;
            if (isPaused)
            {
                audioManager.PlayMusic();
            }
            else
            {
                GameManager.instance.startMusic=true;
                isPaused = true;
            }

            PlayerPrefs.SetInt("Music", 2);
        }
        else
        {
            image.sprite = off;
            musicIsOff = true;
            audioManager.PauseMusic();
            audioManager.isMusicWorking = false;
            PlayerPrefs.SetInt("Music", 1);
        }
    }


}
