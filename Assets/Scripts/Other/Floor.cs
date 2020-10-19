using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject prefab;
    private GameObject player;
    private GameObject nearBlock;
    private GameObject block;
    private PlayerHealth playerHealth;

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
            Destroy(collider.gameObject);
            player = Instantiate<GameObject>(prefab, newPosition, Quaternion.identity);
            playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.healthPoints = healthPoints;
            playerHealth.Damage();
        }
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
