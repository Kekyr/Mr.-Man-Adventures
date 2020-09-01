using UnityEngine;

public class CoinColliderListener : MonoBehaviour
{
    public PlayerWallet playerWallet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickUpCoin();
        }
    }

    private void PickUpCoin()
    {
        playerWallet.AddCoin();
        Destroy(gameObject);
    }
}
