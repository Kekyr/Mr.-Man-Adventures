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
    private Vector3 startPosition;
    private Vector3 firstDestination;
    private Vector3 secondDestination;
    private Vector3 currentDestination;
    public Vector3 targetVelocity;

   

    private void Awake()
    {
        common = FindObjectOfType<Common>();
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position + new Vector3(0.5f, 0);
        firstDestination = startPosition + new Vector3(5.3f, 0);
        secondDestination = startPosition + new Vector3(10f, 0);
        currentDestination = firstDestination;
    }

    private void Start()
    {
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

                if(stopped)
                {
                  rigidBody2D.velocity = Vector3.zero;
                  rollSpeed *= -1;
                  animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));
                  yield return new WaitForSeconds(5);
                  stopped = false;
                }

                targetVelocity = new Vector2(rollSpeed, rigidBody2D.velocity.y);

                rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);


                //if (rigidBody2D.position.x > currentDestination.x && rollSpeed > 0)
                //{
                //    rigidBody2D.velocity = Vector3.zero;
                //    animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));
                //    if (currentDestination == firstDestination)
                //    {
                //        currentDestination = secondDestination;
                //    }
                //    else if (currentDestination == secondDestination)
                //    {
                //        currentDestination = firstDestination;
                //        rollSpeed *= -1;
                //    }
                //    yield return new WaitForSeconds(5);
                //}
                //else if (rigidBody2D.position.x < currentDestination.x && rollSpeed < 0)
                //{
                //    rigidBody2D.velocity = Vector3.zero;
                //    animator.SetFloat("Speed", Mathf.Abs(rigidBody2D.velocity.x));
                //    if (currentDestination == firstDestination)
                //    {
                //        currentDestination = startPosition;
                //    }
                //    else if (currentDestination == startPosition)
                //    {
                //        currentDestination = firstDestination;
                //        rollSpeed *= -1;
                //    }
                //    yield return new WaitForSeconds(5);
                //}

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
