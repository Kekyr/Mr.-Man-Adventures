using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private Collider2D[] colliders;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    public int healthPoints = 3;
    private bool damaging = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        

    }
    public void Damage()
    {
        if (!damaging)
        {
            damaging = true;
            healthPoints -= 1;
            if (healthPoints <= 0)
            {
               
                StartCoroutine(DelayedDestruction());
            }
            else
            {
                StartCoroutine(TemporaryImmortality());
            }
        }
    }

    private IEnumerator TemporaryImmortality()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        animator.SetBool("IsHurt", true);
        yield return new WaitForSeconds(5);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        animator.SetBool("IsHurt", false);
        damaging = false;
    }

    private IEnumerator DelayedDestruction()
    {
        playerMovement.dead = true;
        playerMovement.horizontalMove = 0;
        animator.SetBool("IsDead", true);
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(1);
        rb.AddForce(new Vector2(0f, 700));
        Physics2D.IgnoreLayerCollision(8, 11, true);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
