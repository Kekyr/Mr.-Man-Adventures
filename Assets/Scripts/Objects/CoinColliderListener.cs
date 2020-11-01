using System.Collections;
using UnityEngine;

public class CoinColliderListener : MonoBehaviour
{
    public AudioClip coinSFX;
    
    private PlayerWallet playerWallet;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerWallet = FindObjectOfType<PlayerWallet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PickUpCoin());
        }
    }

    //Сбор монет
    private IEnumerator PickUpCoin()
    {
        audioManager.PlaySFX(coinSFX);
        yield return new WaitForSeconds(0.05f);
        playerWallet.AddCoin();
        Destroy(gameObject);
    }
}
