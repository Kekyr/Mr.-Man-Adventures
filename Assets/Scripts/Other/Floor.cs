using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject prefab;
    private GameObject player;
    private GameObject nearBlock;
    private GameObject block;
    private PlayerHealth playerHealth;
    private SpriteRenderer playerSprite;

    private int healthPoints;
    private Vector3 newPosition;
    private Collider2D[] tiles;
    private float minDistance = 5;
    private float currentDistance;
    private bool IsPositive;
    
    



    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.gameObject;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        healthPoints = playerHealth.healthPoints;
        if (healthPoints == 1)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerHealth.falling = true;
            playerHealth.Damage();
        }
        else
        {
            newPosition = player.transform.position;
            tiles = Physics2D.OverlapCircleAll(newPosition, 2f, 11);
            FindTile();
            StartCoroutine(Delay());
        }
    }

    public IEnumerator Delay()
    {
        playerSprite.enabled = false;
        player.transform.position = newPosition;
        yield return new WaitForSeconds(0.3f);
        playerSprite.enabled = true;
        playerHealth.falling = true;
        playerHealth.Damage();
    }

    private void FindTile()
    {
        foreach (var tile in tiles)
        {
            block = tile.gameObject;
            if (block.transform.position.x > newPosition.x)
            {
                IsPositive = true;
            }
            else
            {
                IsPositive = false;
            }

            currentDistance = newPosition.x - block.transform.position.x;
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearBlock = block;
            }
        }

        if (IsPositive)
        {
            newPosition.x = nearBlock.transform.position.x;
        }
        else
        {
            newPosition.x = nearBlock.transform.position.x - 2;
        }
        newPosition.y = 3;
    }

    
}
