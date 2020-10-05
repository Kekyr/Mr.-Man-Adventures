using UnityEngine;

public class OctiMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    private Animator animator;
    private Common common;

    [SerializeField] public float moveSpeed = 5.5f;//Скорость передвижения
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;//Сглаживание передвижения
    public bool stopped = false;
    public Vector3 targetVelocity;
    private Vector3 velocity = Vector3.zero;
    private bool facingRight = false;//Направление спрайта


    private void Start()
    {
        common = FindObjectOfType<Common>();
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Передвижение Octi
    private void FixedUpdate()
    {
        if (enabled)
        {
            if (stopped)
            {
                moveSpeed *= -1;
                animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));
                stopped = false;
            }

            targetVelocity = new Vector2(moveSpeed, rigidBody2D.velocity.y);

            rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (moveSpeed > 0 && !facingRight)
            {
                common.Flip(ref facingRight, gameObject);
            }

            else if (moveSpeed < 0 && facingRight)
            {
                common.Flip(ref facingRight, gameObject);
            }

            animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));

        }
    }



}
