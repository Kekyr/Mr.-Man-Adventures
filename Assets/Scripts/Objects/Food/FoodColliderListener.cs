using UnityEngine;

public class FoodColliderListener : MonoBehaviour
{
    public AudioClip errorSFX;
    public AudioClip eatSFX;
    private AudioManager audioManager;
    private PlayerHealth playerHealth;
    private PlayerWallet playerWallet;
    public GameObject currentGameObject;

    public string currentTag;
    


    private void Start()
    {
        audioManager = AudioManager.instance;
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerWallet = FindObjectOfType<PlayerWallet>();

        var colliders = GetComponentsInChildren<Collider2D>();
        foreach(var col in colliders)
        {
            if(col.gameObject!=gameObject)
            {
                FoodColliderBridge cb= col.gameObject.AddComponent<FoodColliderBridge>();
                cb.Initialize(this);
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Purchase"))
        {
            if(currentTag=="Burger")
            {
                if(playerHealth.healthPoints==1 && playerWallet.amountOfCoins>=20)
                {
                    audioManager.PlaySFX(eatSFX);
                    playerHealth.healthPoints = 3;
                    playerHealth.hearts[1].ChangeSprite();
                    playerHealth.hearts[2].ChangeSprite();
                    playerWallet.DeleteCoins(20);
                    Destroy(currentGameObject);
                    
                }
                else
                {
                    audioManager.PlaySFX(errorSFX);
                }
            }
            else if(currentTag=="Fries")
            {
                if (playerHealth.healthPoints==2 && playerWallet.amountOfCoins >= 10)
                {
                    audioManager.PlaySFX(eatSFX);
                    playerHealth.healthPoints = 3;
                    playerHealth.hearts[2].ChangeSprite();
                    playerWallet.DeleteCoins(10);
                    Destroy(currentGameObject);
                }
                else if (playerHealth.healthPoints == 1 && playerWallet.amountOfCoins >= 10)
                {
                    audioManager.PlaySFX(eatSFX);
                    playerHealth.healthPoints = 2;
                    playerHealth.hearts[1].ChangeSprite();
                    playerWallet.DeleteCoins(10);
                    Destroy(currentGameObject);
                }
                else
                {
                    audioManager.PlaySFX(errorSFX);
                }
            }
        }
        
    }

    

    
    
}
