using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public AudioClip[] menuAudioClips;
    public AudioClip[] gameAudioClips;
    private AudioManager audioManager;

    public bool startMusic;
    private int menuClipNumber;
    private int gameClipNumber;

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
    }
    private void Start()
    {
        audioManager = AudioManager.instance;
        menuClipNumber = Random.Range(0, menuAudioClips.Length);
        gameClipNumber = Random.Range(0, gameAudioClips.Length);
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
            if (SceneManager.GetActiveScene().buildIndex==0)
            {
                audioManager.StopUpdateMusicWithFade();
                audioManager.StopMusic();
                audioManager.StartMusic(menuClipNumber, menuAudioClips);
            }
            else if(SceneManager.GetActiveScene().buildIndex==3)
            {
                audioManager.StopUpdateMusicWithFade();
                audioManager.StopMusic();
                audioManager.StartMusic(gameClipNumber, gameAudioClips);
            }
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
