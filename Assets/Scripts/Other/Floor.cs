using System;
using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject nearBlock;
    [SerializeField] private LayerMask blocks;
    private GameObject player;
    private GameObject block;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private SpriteRenderer playerSprite;
    private Rigidbody2D rigidBody2D;

    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Collider2D[] tiles;
    private float minDistance = 5;
    [SerializeField] private float currentDistance;
    private int healthPoints;
    [SerializeField] private float direction;
    private float offset;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerMovement = player.GetComponent<PlayerMovement>();
        rigidBody2D = player.GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = Vector3.zero;
        healthPoints = playerHealth.healthPoints;
        playerHealth.falling = true;
        direction = playerMovement.lastHorizontalMove;
        
        if (healthPoints == 1)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerHealth.Damage();
        }
        else
        {
            newPosition = player.transform.position;
            if (direction > 0)
            {
                offset = -1f;
            }
            else
            {
                offset = 1f;
            }
            newPosition.x += offset;
            newPosition.y = 3;
            tiles = Physics2D.OverlapCircleAll(newPosition, 1.5f, blocks);
            FindTile();
            StartCoroutine(Delay());
        }
    }

    public IEnumerator Delay()
    {
        playerHealth.Damage();
        playerSprite.enabled = false;
        player.transform.position = newPosition;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1f);
        playerSprite.enabled = true;
        Debug.Log("Floor damage");
        playerMovement.enabled = true;
    }

    private void FindTile()
    {
        nearBlock = null;
        minDistance = 5;
        currentDistance = 0;

        foreach (var tile in tiles)
        {
            block = tile.gameObject;

            currentDistance = Math.Abs(newPosition.x - block.transform.position.x);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearBlock = block;
            }
        }

        newPosition.x = nearBlock.transform.position.x;
    }


}
