using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTheLevelColliderListener : MonoBehaviour
{
    private AudioManager audioManager;
    private MovingCamera movingCamera;

    public string currentTag;

    private void Start()
    {
        movingCamera = FindObjectOfType<MovingCamera>();
        audioManager = AudioManager.instance;

        var colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                EndOfTheLevelColliderBridge cb = col.gameObject.AddComponent<EndOfTheLevelColliderBridge>();
                cb.Initialize(this);
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall"))
        {
            if (currentTag == "CameraEnd")
            {
                movingCamera.enabled = false;
            }
        }
        else if(collider.gameObject.CompareTag("Player"))
        {
            if (currentTag == "PlayerEnd")
            {
                audioManager.StopMusic();
                audioManager.StopUpdateMusicWithFade();
                SceneManager.LoadScene("Ending");
            }
        }
    }




}
