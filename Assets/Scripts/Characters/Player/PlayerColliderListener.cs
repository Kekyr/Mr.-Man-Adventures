using System.Collections;
using UnityEngine;

public class PlayerColliderListener : MonoBehaviour
{
    public AudioClip deathSFX;
    private AudioManager audioManager;
    public Animator animator;

    //Подключение коллайдеров детей
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponentInParent<Animator>();
        var collider = GetComponentInChildren<Collider2D>();
        if (collider.gameObject != gameObject)
        {
           PlayerColliderBridge cb = collider.gameObject.AddComponent<PlayerColliderBridge>();
           cb.Initialize(this);
        }
        
    }

    //Удар кулаком
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("RollingNeroBody"))
        {
            audioManager.PlaySFX(deathSFX, 0.2f);
            StartCoroutine(DelayedParentDestruction(collider));
        }
        else if(collider.gameObject.CompareTag("ChiChi"))
        {
            audioManager.PlaySFX(deathSFX, 0.2f);
            StartCoroutine(DelayedDestruction(collider));
        }
        
    }

    //Уничтожение врага (ChiChi) после удара кулаком
    private IEnumerator DelayedDestruction(Collider2D collider)
    {
        collider.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        collider.gameObject.GetComponent<ChiChiMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(5);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Destroy(collider.gameObject);
        
    }
    
    //Уничтожение врага (Rolling Nero) после удара кулаком
    private IEnumerator DelayedParentDestruction(Collider2D collider)
    {
        collider.gameObject.GetComponentInParent<Animator>().SetBool("IsDead", true);
        collider.gameObject.GetComponentInParent<RollingNeroMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(5);
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Destroy(collider.gameObject.transform.parent.gameObject); 
    }



}
