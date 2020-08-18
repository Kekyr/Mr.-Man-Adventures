using UnityEngine;

public class ChiChiColliderListener : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.Damage();
        }
    }
}
