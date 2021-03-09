using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    
    private AudioSource musicSource;
    //private AudioSource musicSource2;
    private AudioSource sfxSource;
    //private AudioSource activeSource;
    private IEnumerator updateMusicWithFade;

    public bool isSFXWorking;
    public bool isMusicWorking;
    //private bool firstMusicSourceIsPlaying;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = GetComponent<AudioSource>();
        //musicSource2 = GetComponent<AudioSource>();
        sfxSource = GetComponent<AudioSource>();

        isSFXWorking = true;
        isMusicWorking = true;
    }


    public void PlayMusic(AudioClip musicClip)
    {
        //AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;
        
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlayMusic()
    {
        //AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;

        musicSource.Play();
    }

    public void StopMusic()
    {
        //AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;

        musicSource.Stop();
        StopUpdateMusicWithFade();
    }

    public void PauseMusic()
    {
        //AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;

        musicSource.Pause();
    }

    public void StartMusic(int clipNumber, AudioClip[] audioClips)
    {
        if (isMusicWorking)
        {
            if (clipNumber == 0)
            {
                PlayMusic(audioClips[clipNumber]);
                PlayMusicWithFade(audioClips[clipNumber + 1], audioClips[clipNumber].length);
            }
            else
            {
                PlayMusic(audioClips[clipNumber]);
                PlayMusicWithFade(audioClips[clipNumber - 1], audioClips[clipNumber].length);
            }
        }
    }


    public void PlayMusicWithFade(AudioClip newClip, float transitionTime=1.0f)
    {
        //activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;

        updateMusicWithFade = UpdateMusicWithFade(musicSource, newClip, transitionTime);
        
        StartCoroutine(updateMusicWithFade);
        
        
    }


    private IEnumerator UpdateMusicWithFade(AudioSource musicSource, AudioClip newClip, float transitionTime)
    {
        float t = 0.0f;

        for(t=0; t<transitionTime; t+=Time.deltaTime)
        {
            musicSource.volume += (1 - (t / transitionTime));
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            musicSource.volume += (t / transitionTime);
            yield return null;
        }
    }

    public void StopUpdateMusicWithFade()
    {
        if (updateMusicWithFade != null)
        {
            StopCoroutine(updateMusicWithFade);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isSFXWorking)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (isSFXWorking)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
