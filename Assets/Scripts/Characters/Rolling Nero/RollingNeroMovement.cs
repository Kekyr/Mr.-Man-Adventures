using System.Collections;
using UnityEngine;

public class RollingNeroMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    private Animator animator;
    private Common common;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;//Сглаживание передвижения
    [SerializeField] public float rollSpeed = 10f;//Скорость передвижения
    public bool stopped = false;
    private bool facingRight = false;//Направление спрайта
    private Vector3 velocity = Vector3.zero;
    public Vector3 targetVelocity;

    private void Start()
    {
        common = FindObjectOfType<Common>();
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }

    //Передвижение Rolling Nero
    private IEnumerator Move()
    {
        while (true)
        {
            if (!enabled)
            {
                yield break;
            }

            if (stopped)
            {
                rigidBody2D.velocity = Vector3.zero;
                rollSpeed *= -1;
                animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));
                yield return new WaitForSeconds(2);
                stopped = false;
            }

            targetVelocity = new Vector2(rollSpeed, rigidBody2D.velocity.y);

            rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (rollSpeed > 0 && !facingRight)
            {
                common.Flip(ref facingRight, gameObject);
            }

            else if (rollSpeed < 0 && facingRight)
            {
                common.Flip(ref facingRight, gameObject);
            }

            animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));

            yield return null;


        }
    }
}
