using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public AudioClip[] menuAudioClips;
    public AudioClip[] gameAudioClips;
    private AudioManager audioManager;
    private TextLocalizer textLocalizer;

    public bool startMusic;
    private int menuClipNumber;
    private int gameClipNumber;
    private string gameId = "4067579";
    private bool testMode = false;


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
        Advertisement.Initialize(gameId,testMode);
        audioManager = AudioManager.instance;
        textLocalizer = GetComponent<TextLocalizer>();
        
        startMusic = true;
    }

    private void Update()
    {
        if(startMusic)
        {
            CheckScene();
        }

        if(PauseMenu.changeLanguage || Menu.changeLanguage)
        {
            textLocalizer.CheckFont();
        }
    }

    private void CheckScene()
    {
        
        if (SceneManager.GetActiveScene().name=="Menu")
        {
            audioManager.StopMusic();
            menuClipNumber = Random.Range(0, menuAudioClips.Length);
            audioManager.StartMusic(menuClipNumber, menuAudioClips);
        }
        else if (SceneManager.GetActiveScene().name=="Level 1")
        {
            audioManager.StopMusic();
            gameClipNumber = Random.Range(0, gameAudioClips.Length);
            audioManager.StartMusic(gameClipNumber, gameAudioClips);

        }
        startMusic = false;
    }

   

    

    
}
