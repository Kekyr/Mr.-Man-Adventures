using UnityEngine;

public class OctiMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    private Animator animator;
    private Common common;

    [SerializeField] public float moveSpeed = 6.5f;//Скорость передвижения
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;//Сглаживание передвижения
    public bool stopped = false;
    private bool facingRight = false;//Направление спрайта
    public Vector3 targetVelocity;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        common = FindObjectOfType<Common>();
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (enabled)
        {
            if (stopped)
            {
                rigidBody2D.velocity = Vector3.zero;
                moveSpeed *= -1;
                animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));
                stopped = false;
            }

            targetVelocity = new Vector2(moveSpeed, rigidBody2D.velocity.y);

            rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            //rigidBody2D.AddForce(targetVelocity);//14.5 скорость


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
