﻿using UnityEngine;

public class ChiChiColliderListener : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private ChiChiMovement chichiMovement;
    public string currentTag;//Тэг сработавшего коллайдера

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        chichiMovement = FindObjectOfType<ChiChiMovement>();

        var colliders = GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                ChiChiColliderBridge cb = col.gameObject.AddComponent<ChiChiColliderBridge>();
                cb.Initialize(this);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentTag == "ChiChi")
            {
                playerHealth.Damage();
            }
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            if (currentTag == "ChiChi")
            {
                chichiMovement.stopped = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (currentTag == "Trap")
            {
                Debug.Log("We here!");
                Debug.Log(chichiMovement.isPlayerHere);
                chichiMovement.isPlayerHere = true;
            }
        }
    }
}
