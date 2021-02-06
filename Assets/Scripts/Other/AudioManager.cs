using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource[] musicSources;
    private AudioSource sfxSource;

    public bool isSFXWorking;
    public bool isMusicWorking;
    private bool firstMusicSourceIsPlaying;
    


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

        musicSources = GetComponents<AudioSource>();
        musicSource = musicSources[0];
        musicSource2 = musicSources[1];
        sfxSource = gameObject.AddComponent<AudioSource>();

        isSFXWorking = true;
        isMusicWorking = true;
        musicSource.loop = true;
        musicSource2.loop = true;
    }


    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;
        
        activeSource.clip = musicClip;
        activeSource.Play();

    }

    public void StopMusic()
    {
        AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;

        activeSource.Stop();
    }

    public void StartMusic(int clipNumber, AudioClip[] audioClips)
    {
        if (isMusicWorking)
        {
            if (clipNumber == 0)
            {
                PlayMusic(audioClips[clipNumber]);
                PlayMusicWithFade(audioClips[clipNumber + 1], 59);
            }
            else
            {
                PlayMusic(audioClips[clipNumber]);
                PlayMusic(audioClips[clipNumber]);
                PlayMusicWithFade(audioClips[clipNumber - 1], 59);
            }
        }
    }


    public void PlayMusicWithFade(AudioClip newClip, float transitionTime=1.0f)
    {
        AudioSource activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));

    }


    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if(!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        float t = 0.0f;

        for(t=0; t<transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume += (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume += (t / transitionTime);
            yield return null;
        }
    }

    public void StopUpdateMusicWithFade()
    {
        StopCoroutine("UpdateMusicWithFade");
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
        musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
