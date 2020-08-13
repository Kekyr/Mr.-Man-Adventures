using System.Collections;
using UnityEngine;

public class NeroColliderListener : MonoBehaviour
{
    public string currentTag;//Тэг сработавшего коллайдера

    //Подключение коллайдеров детей
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

    //Взаимодействие с Rolling Nero
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentTag == "Head")
            {
                StartCoroutine(DelayedDestruction(collision));
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

    //Уничтожение врага (Rolling Nero) после атаки сверху
    private IEnumerator DelayedDestruction(Collision2D collision)
    {
        gameObject.GetComponent<Animator>().SetBool("IsSquished", true);
        gameObject.GetComponent<RollingNeroMovement>().enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }



}

