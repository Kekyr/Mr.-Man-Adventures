using System.Collections;
using UnityEngine;

public class PlayerColliderListener : MonoBehaviour
{
    public Animator animator;

    //Подключение коллайдеров детей
    void Awake()
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
            StartCoroutine(DelayedDestruction(collision));
        }
    }

    //Уничтожение врага (Rolling Nero) после удара кулаком
    private IEnumerator DelayedDestruction(Collider2D collision)
    {
        collision.gameObject.GetComponentInParent<Animator>().SetBool("IsDead", true);
        collision.gameObject.GetComponentInParent<RollingNeroMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(5);
        Destroy(collision.gameObject.transform.parent.gameObject);
    }



}
