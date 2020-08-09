using System.Collections;
using UnityEngine;

public class NeroColliderListener : MonoBehaviour
{
    public string currentTag;
    private void Awake()
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders) 
        {
            if (col.gameObject != gameObject)
            {
                NeroColliderBridge cb = col.gameObject.AddComponent<NeroColliderBridge>();
                cb.Initialize(this);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentTag == "Head")
            {
                Debug.Log("Head Collider");
                Destroy(gameObject);
            }
            else if (currentTag == "Top")
            {
                Debug.Log("Top Collider");
                collision.gameObject.GetComponent<PlayerHealth>().Damage();
            }
            else if(currentTag=="Body")
            {
                Debug.Log("Body Collider");
                collision.gameObject.GetComponent<PlayerHealth>().Damage();
            }
        }
        
    }

    

}

