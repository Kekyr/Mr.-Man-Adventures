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
        
        startMusic = true;
    }

    private void Update()
    {
        if(startMusic)
        {
            CheckScene();
        }
    }

    private void CheckScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioManager.StopMusic();
            menuClipNumber = Random.Range(0, menuAudioClips.Length);
            audioManager.StartMusic(menuClipNumber, menuAudioClips);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            audioManager.StopMusic();
            gameClipNumber = Random.Range(0, gameAudioClips.Length);
            audioManager.StartMusic(gameClipNumber, gameAudioClips);

        }
        startMusic = false;
    }

    

    
}
