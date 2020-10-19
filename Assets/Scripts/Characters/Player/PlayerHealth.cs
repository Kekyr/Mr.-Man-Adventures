using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public AudioClip deathSFX;
    public AudioClip hurtSFX;
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private AudioManager audioManager;
    public List<Heart> hearts = new List<Heart>();

    public int healthPoints = 3; //Очки здоровья игрока
    public bool falling = false;
    private bool damaging = false;
    private Vector2 lastJump = new Vector2(0, 0.07f);//Сила с которой игрок полетит вверх после смерти
    private Heart temp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        hearts = (FindObjectsOfType<Heart>()).ToList();
        temp = hearts[0];
        hearts[0] = hearts[1];
        hearts[1] = temp;
        hearts.Reverse();
    }

    //Нанесение урона игроку
    public void Damage()
    {
        if (!damaging || falling)
        {
            damaging = true;
            //hearts[healthPoints - 1].ChangeSprite();
            healthPoints -= 1;
            audioManager.PlaySFX(hurtSFX);
            if (healthPoints <= 0)
            {
                audioManager.StopMusic();
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
        yield return new WaitForSeconds(5);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        animator.SetBool("IsHurt", false);
        damaging = false;
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
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
