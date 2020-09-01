using System.Collections;
using UnityEngine;

public class PlayerColliderListener : MonoBehaviour
{
    public Animator animator;

    //Подключение коллайдеров детей
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        var collider = GetComponentInChildren<Collider2D>();
        if (collider.gameObject != gameObject)
        {
           PlayerColliderBridge cb = collider.gameObject.AddComponent<PlayerColliderBridge>();
           cb.Initialize(this);
        }
        
    }

    //Удар кулаком
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            StartCoroutine(DelayedParentDestruction(collision));
        }
        else if(collision.gameObject.CompareTag("ChiChi"))
        {
            StartCoroutine(DelayedDestruction(collision));
        }
        
    }

    //Уничтожение врага (ChiChi) после удара кулаком
    private IEnumerator DelayedDestruction(Collider2D collision)
    {
        collision.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
        collision.gameObject.GetComponent<ChiChiMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(5);
        Destroy(collision.gameObject);
        
    }
    
    //Уничтожение врага (Rolling Nero) после удара кулаком
    private IEnumerator DelayedParentDestruction(Collider2D collision)
    {
        collision.gameObject.GetComponentInParent<Animator>().SetBool("IsDead", true);
        collision.gameObject.GetComponentInParent<RollingNeroMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(5);
        Destroy(collision.gameObject.transform.parent.gameObject); 
    }



}
