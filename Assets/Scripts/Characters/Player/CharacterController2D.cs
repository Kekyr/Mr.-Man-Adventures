using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[SerializeField] private LayerMask whatIsGround;// ���� � ������������ ����������� �������������
	[SerializeField] private Transform groundCheck;// ����� ����� �������� �����������
	private Rigidbody2D rigidBody2D;
	private Common common;

	[SerializeField] private float jumpForce = 400f;                        // ���� � ������� ������� �����
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;//����������� ������������
	const float groundedRadius = .2f; // ������ ����� �������� �����������
	private bool grounded;            // �������� ����������� ������
	private bool facingRight = true;  //����������� �������
	private Vector3 velocity = Vector3.zero;

	private void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		common = FindObjectOfType<Common>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	//�������� ����������� ������
	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	//������������ ������
	public void Move(float move,bool jump)
	{
		Vector3 targetVelocity = new Vector2(move * 10f, rigidBody2D.velocity.y);

		rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

		if (move > 0 && !facingRight)
		{
			common.Flip(ref facingRight,gameObject);
		}
		else if (move < 0 && facingRight)
		{
			common.Flip(ref facingRight, gameObject);
		}

		if (grounded && jump)
		{
			grounded = false;
			rigidBody2D.AddForce(new Vector2(0f, jumpForce));
		}
	}

}

