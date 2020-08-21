﻿using System.Collections;
using UnityEngine;

public class NeroColliderListener : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private RollingNeroMovement rollingNeroMovement;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    public string currentTag;//Тэг сработавшего коллайдера
    

    //Подключение коллайдеров детей
    private void Awake()
    {
        rollingNeroMovement = GetComponent<RollingNeroMovement>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
                playerHealth.Damage();
            }
            else if(currentTag=="Body")
            {
                Debug.Log("Body Collider");
                playerHealth.Damage();
            }
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            if (currentTag == "Body")
            {
                Debug.Log("Body Collider");
                rollingNeroMovement.stopped = true;

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

