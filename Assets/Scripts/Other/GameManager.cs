using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioManager audioManager;

    public bool startMusic;
    private int clipNumber;
    

    private void Start()
    {
        audioManager = AudioManager.instance;
        clipNumber = Random.Range(0, audioClips.Length);
        startMusic = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            RestartGame();
        }

        if(startMusic)
        {
            audioManager.StartMusic(clipNumber, audioClips);
            startMusic = false;
        }
    }

    public void RestartGame()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 12, false);
        Physics2D.IgnoreLayerCollision(8, 0, false);
        LoadCurrentScene();
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
