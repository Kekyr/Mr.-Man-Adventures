using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioManager audioManager;
    private int clipNumber;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartMusic();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Restart"))
        {
            RestartGame();
        }

    }

    private void StartMusic()
    {
        clipNumber = Random.Range(0, audioClips.Length);

        if (clipNumber == 0)
        {
            audioManager.PlayMusic(audioClips[clipNumber]);
            audioManager.PlayMusicWithFade(audioClips[clipNumber + 1],59);
        }
        else
        {
            audioManager.PlayMusic(audioClips[clipNumber]);
            audioManager.PlayMusicWithFade(audioClips[clipNumber - 1],59);
        }
    }

    public void RestartGame()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 12, false);
        Physics2D.IgnoreLayerCollision(8, 0, false);
        SceneManager.LoadScene(0);
        
    }

    
}
