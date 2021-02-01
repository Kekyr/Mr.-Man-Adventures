using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{

    public Sprite on;
    public Sprite off;
    private Image image;
    private Menu menu;
    private AudioManager audioManager;
    public bool isOff;


    private void Start()
    {
        image = GetComponent<Image>();
        audioManager = AudioManager.instance;
        menu = GetComponentInParent<Menu>();
    }

    public void MusicSwitch()
    {
        if (isOff)
        {
            image.sprite = on;
            isOff = false;
            menu.startMusic = true;
            audioManager.isMusicWorking = true;
        }
        else
        {
            image.sprite = off;
            isOff = true;
            audioManager.StopUpdateMusicWithFade();
            audioManager.StopMusic();
            audioManager.isMusicWorking = false;
        }
    }

    public void SFXSwitch()
    {
        if (isOff)
        {
            image.sprite = on;
            isOff = false;
            audioManager.isSFXWorking = true;
        }
        else
        {
            image.sprite = off;
            isOff = true;
            audioManager.isSFXWorking = false;
        }
    }


}
