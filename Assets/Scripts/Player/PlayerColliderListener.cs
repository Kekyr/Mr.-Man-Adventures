using System.Collections;
using UnityEngine;

public class PlayerColliderListener : MonoBehaviour
{
    public Animator animator;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        { 
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }

    


}
