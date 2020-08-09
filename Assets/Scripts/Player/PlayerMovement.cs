using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Collider2D fistCollider;

    [SerializeField] private float runSpeed = 15f;

    public float horizontalMove = 0f;
    public bool dead = false;
    private bool jump = false;


    private void Start()
    { 
        fistCollider.enabled = false;
    }

    private void Update()
    {
        if (!dead)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (Input.GetButtonDown("Punch"))
            {
                fistCollider.enabled = true;
                animator.SetBool("IsPunching", true);
            }

            if (Input.GetButtonUp("Punch"))
            {
                fistCollider.enabled = false;
                animator.SetBool("IsPunching", false);
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,jump);
        jump = false;
    }
}