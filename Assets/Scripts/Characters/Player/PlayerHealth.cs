using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerHealth : MonoBehaviour
{
   
    private static int countOfDeath;

    public GameObject rewardedAd;
    public List<Heart> hearts = new List<Heart>();

    public AudioClip deathSFX;
    public AudioClip hurtSFX;
    public AudioClip healSFX;
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;
    

    public int healthPoints = 3; //Очки здоровья игрока
    public bool falling = false;
    
    private bool damaging = false;
    private bool isFirstTime = true;
    private Vector2 lastJump = new Vector2(0, 0.10f);//Сила с которой игрок полетит вверх после смерти
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        audioManager = AudioManager.instance;
    }

    //Нанесение урона игроку
    public void Damage()
    {
        if (!damaging || falling)
        {
            damaging = true;
            hearts[healthPoints - 1].ChangeSprite();
            healthPoints -= 1;
            
            audioManager.PlaySFX(hurtSFX);
            if (healthPoints <= 0)
            {
                audioManager.StopMusic();
                audioManager.StopUpdateMusicWithFade();
                audioManager.PlaySFX(deathSFX);
                StartCoroutine(DelayedDestruction());
            }
            else
            {
                StartCoroutine(TemporaryImmortality());
            }

            

        }
    }

    //Временная неуязвимость после урона
    private IEnumerator TemporaryImmortality()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        animator.SetBool("IsHurt", true);
        yield return new WaitForSeconds(2);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        animator.SetBool("IsHurt", false);
        damaging = false;
        falling = false;

        if (healthPoints == 1 && isFirstTime)
        {
            Time.timeScale = 0f;
            playerMovement.enabled = false;
            PauseMenu.gameIsPaused = true;
            RewardedAdsButton.isRewardedAdOn = true;
            rewardedAd.SetActive(true);
            isFirstTime = false;
        }


    }

    //Уничтожение игрока при очках здоровья равных нулю
    public IEnumerator DelayedDestruction()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().horizontalMove = 0;
        rigidBody2D.velocity = Vector2.zero;
        animator.SetBool("IsDead", true);
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(1);
        rigidBody2D.AddForce(lastJump);
        if (rigidBody2D.gravityScale != 3)
        {
            rigidBody2D.gravityScale = 3;
        }
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(8, 12, true);
        Physics2D.IgnoreLayerCollision(8, 0, true);
        yield return new WaitForSeconds(3);
        countOfDeath++;
        if(countOfDeath %5==0)
        {
            ShowInterstitialAd();
        }
        Common.LoadNextScene();
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

   

    

}
