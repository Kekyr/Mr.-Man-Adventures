using System.Collections;
using UnityEngine;

public class RollingNeroMovement : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private float rollSpeed = 10f;

    private bool facingRight = false;
    private Vector3 velocity = Vector3.zero;
    private Vector3 startPosition;
    private Vector3 firstDestination;
    private Vector3 secondDestination;
    private Vector3 currentDestination;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position+new Vector3(0.5f,0);
        firstDestination = startPosition + new Vector3(5.3f, 0);
        secondDestination = startPosition + new Vector3(10f, 0);
        currentDestination = firstDestination;

    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            Vector3 targetVelocity = new Vector2(rollSpeed, rb.velocity.y);

            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            
            if (rb.position.x>currentDestination.x && rollSpeed>0)
            {
                rb.velocity = Vector3.zero;
                animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
                if (currentDestination== firstDestination)
                {
                    currentDestination = secondDestination;
                }
                else if(currentDestination== secondDestination)
                {
                    currentDestination = firstDestination;
                    rollSpeed *= -1;
                }
                yield return new WaitForSeconds(5); 
            }
            else if(rb.position.x<currentDestination.x && rollSpeed<0)
            {
                rb.velocity = Vector3.zero;
                animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
                if (currentDestination == firstDestination)
                {
                    currentDestination = startPosition;
                }
                else if (currentDestination == startPosition)
                {
                    currentDestination = firstDestination;
                    rollSpeed *= -1;
                }
                yield return new WaitForSeconds(5);
            }

            if (rollSpeed > 0 && !facingRight)
            {
                Flip();
            }

            else if (rollSpeed < 0 && facingRight)
            {
                Flip();
            }

            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            yield return null;
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;

        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
