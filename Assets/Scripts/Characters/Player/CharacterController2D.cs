using System.Collections;
using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    public AudioClip jumpSFX;

    [SerializeField] private LayerMask whatIsGround;// Слои с коллайдерами являющимися поверхностями
    [SerializeField] private Transform groundCheck;// Центр круга проверки приземления
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private Common common;
    private AudioManager audioManager;

    [SerializeField] private float jumpForce = 0.05f;                        // Сила с которой прыгает игрок
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;//Сглаживание передвижения
    private const float groundedRadius = .2f; // Радиус круга проверки приземления
    public bool grounded;            // Проверка приземления игрока
    private bool facingRight = true;  //Направление спрайта
    private bool isJumping = false;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        common = FindObjectOfType<Common>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    //Проверка приземления игрока
    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }

    //Передвижение игрока
    public void Move(float move,bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rigidBody2D.velocity.y);

        rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (move > 0 && !facingRight)
        {
            common.Flip(ref facingRight, gameObject);
        }
        else if (move < 0 && facingRight)
        {
            common.Flip(ref facingRight, gameObject);
        }

        if (grounded && jump && !isJumping)
        {
            animator.SetTrigger("IsJumping");
            audioManager.PlaySFX(jumpSFX);
            grounded = false;
            isJumping = true;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
            StartCoroutine(Jumping());
        }
        
    }

    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
    }

}

