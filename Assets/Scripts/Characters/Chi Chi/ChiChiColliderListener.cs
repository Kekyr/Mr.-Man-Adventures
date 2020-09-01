using UnityEngine;

public class ChiChiColliderListener : MonoBehaviour
{
    private PlayerHealth playerHealth;
    
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.Damage();
        }
    }
}
