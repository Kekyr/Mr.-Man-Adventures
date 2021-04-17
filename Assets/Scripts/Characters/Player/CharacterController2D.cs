using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    public AudioClip jumpSFX;

    [SerializeField] private LayerMask whatIsGround;// ���� � ������������ ����������� �������������
    [SerializeField] private Transform groundCheck;// ����� ����� �������� �����������
    private Rigidbody2D rigidBody2D;
    private Common common;
    private AudioManager audioManager;

    [SerializeField] private float jumpForce = 0.05f;                        // ���� � ������� ������� �����
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;//����������� ������������
    const float groundedRadius = .2f; // ������ ����� �������� �����������
    private bool grounded;            // �������� ����������� ������
    private bool facingRight = true;  //����������� �������
    private bool isJumping = false;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        common = FindObjectOfType<Common>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    //�������� ����������� ������
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

    //������������ ������
    public void Move(float move, bool jump)
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

        if (grounded && jump)
        {
            audioManager.PlaySFX(jumpSFX);
            grounded = false;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

}

