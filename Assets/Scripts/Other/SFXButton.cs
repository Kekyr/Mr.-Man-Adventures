using UnityEngine;
using UnityEngine.UI;

public class SFXButton : MonoBehaviour
{
    public static bool sfxIsOff;

    public Sprite on;
    public Sprite off;
    private Image image;
    private AudioManager audioManager;
    



    private void Start()
    {
        audioManager = AudioManager.instance;
        image = GetComponent<Image>();

        if (PlayerPrefs.GetInt("SFX") != 0)
        {
            if (PlayerPrefs.GetInt("SFX") == 1)
            {
                sfxIsOff = true;
                audioManager.isSFXWorking = false;
            }
            else
            {
                sfxIsOff = false;
                audioManager.isSFXWorking = true;
            }
        }

        if (sfxIsOff)
        {
            image.sprite = off;
        }
        else
        {
            image.sprite = on;
        }
    }

    public void SFXSwitch()
    {
        if (sfxIsOff)
        {
            image.sprite = on;
            sfxIsOff = false;
            audioManager.isSFXWorking = true;
            PlayerPrefs.SetInt("SFX", 2);
        }
        else
        {
            image.sprite = off;
            sfxIsOff = true;
            audioManager.isSFXWorking = false;
            PlayerPrefs.SetInt("SFX", 1);
        }
    }


}
