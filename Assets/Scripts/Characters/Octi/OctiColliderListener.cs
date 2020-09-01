using System.Collections;
using UnityEngine;

public class OctiColliderListener : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private OctiMovement octiMovement;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    public string currentTag;//Тэг сработавшего коллайдера


    //Подключение коллайдеров детей
    private void Start()
    {
        octiMovement = GetComponent<OctiMovement>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();

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

    //Взаимодействие с Rolling Nero
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentTag == "Head")
            {
                StartCoroutine(DelayedDestruction());
            }
            else if (currentTag == "Body")
            {
                playerHealth.Damage();
            }
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            if (currentTag == "Body")
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
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
