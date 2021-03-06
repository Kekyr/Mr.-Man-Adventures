﻿using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public static bool musicIsOff;
    public static bool sfxIsOff;

    public Sprite on;
    public Sprite off;
    private Image image;
    private GameManager gameManager;
    private AudioManager audioManager;


    private void Start()
    {
        image = GetComponent<Image>();
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;

        if(musicIsOff)
        {
            image.sprite = off;
        }
        else
        {
            image.sprite = on;
        }

        if(sfxIsOff)
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
        }
        else
        {
            image.sprite = off;
            musicIsOff = true;
            audioManager.StopUpdateMusicWithFade();
            audioManager.StopMusic();
            audioManager.isMusicWorking = false;
        }
    }

    public void SFXSwitch()
    {
        if (sfxIsOff)
        {
            image.sprite = on;
            sfxIsOff = false;
            audioManager.isSFXWorking = true;
        }
        else
        {
            image.sprite = off;
            sfxIsOff = true;
            audioManager.isSFXWorking = false;
        }
    }


}
