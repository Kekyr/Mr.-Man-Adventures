using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Heart[] hearts;
    public AudioClip deathSFX;
    public AudioClip hurtSFX;
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private AudioManager audioManager;

    public int healthPoints = 3; //Очки здоровья игрока
    private bool damaging = false;
    private Vector2 lastJump=new Vector2(0,0.07f);//Сила с которой игрок полетит вверх после смерти

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    //Нанесение урона игроку
    public void Damage()
    {
        if (!damaging)
        {
            damaging = true;
            hearts[healthPoints - 1].ChangeSprite();
            healthPoints -= 1;
            if (healthPoints <= 0)
            {
                audioManager.PlaySFX(hurtSFX, 1f);
                audioManager.StopMusic();
                audioManager.PlaySFX(deathSFX);
                StartCoroutine(DelayedDestruction());
            }
            else
            {
                audioManager.PlaySFX(hurtSFX,1);
                StartCoroutine(TemporaryImmortality());
            }
        }
    }

    //Временная неуязвимость после урона
    private IEnumerator TemporaryImmortality()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        animator.SetBool("IsHurt", true);
        yield return new WaitForSeconds(5);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        animator.SetBool("IsHurt", false);
        damaging = false;
    }

    //Уничтожение игрока при очках здоровья равных нулю
    private IEnumerator DelayedDestruction()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().horizontalMove = 0;
        rigidBody2D.velocity = Vector2.zero;
        animator.SetBool("IsDead", true);
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(1);
        rigidBody2D.AddForce(lastJump);
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(8, 12, true);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
