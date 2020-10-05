using System;
using System.Linq;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject prefab;
    private GameObject player;
    private Vector3 newPosition;
    private PlayerHealth playerHealth;
    private int healthPoints;
    
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        healthPoints = playerHealth.healthPoints;
        if (healthPoints == 1)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerHealth.Damage();
        }
        else
        {
            newPosition = player.transform.position;
            newPosition.x -=1;
            newPosition.y = 3;
            Destroy(collider.gameObject);
            player = Instantiate<GameObject>(prefab, newPosition, Quaternion.identity);
            playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.healthPoints = healthPoints;
            playerHealth.Damage();
        }


    }
}
