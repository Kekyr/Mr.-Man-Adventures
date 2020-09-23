using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip track1;
    public AudioClip track2;
    private AudioManager audioManager;
    
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic(track1);
        audioManager.PlayMusicWithFade(track2,59);
        
    }

    
}
