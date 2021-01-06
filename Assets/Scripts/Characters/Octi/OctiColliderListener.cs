using System.Collections;
using UnityEngine;

public class OctiColliderListener : MonoBehaviour
{
    public AudioClip deathSFX;
    private Puff puff;
    private AudioManager audioManager;
    private PlayerHealth playerHealth;
    private OctiMovement octiMovement;
    private Rigidbody2D rigidBody2D;
    private Animator animator;

    public string currentTag;//Тэг сработавшего коллайдера


    //Подключение коллайдеров детей
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioManager = FindObjectOfType<AudioManager>();
        puff = FindObjectOfType<Puff>();
        octiMovement = GetComponent<OctiMovement>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

        var colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                OctiColliderBridge cb = col.gameObject.AddComponent<OctiColliderBridge>();
                cb.Initialize(this);
            }
        }
    }

    //Взаимодействие с Octi
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentTag == "Head")
            {
                audioManager.PlaySFX(deathSFX,0.2f);
                StartCoroutine(DelayedDestruction());
            }
            else if (currentTag == "OctiBody")
            {
                playerHealth.Damage();
            }
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            if (currentTag == "OctiBody")
            {
                octiMovement.stopped = true;
            }
        }

    }
    
    //Уничтожение врага (Octi) после атаки сверху
    private IEnumerator DelayedDestruction()
    {
        gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        gameObject.GetComponent<OctiMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        puff.transform.position = transform.position;
        puff.destruction = true;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
