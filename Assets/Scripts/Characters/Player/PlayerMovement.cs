using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip punchSFX;
    public CharacterController2D controller;
    public Animator animator;
    public Collider2D fistCollider;//Коллайдер кулака, включается после удара и выключается по его окончании
    public Collider2D purchaseCollider;
    private AudioManager audioManager;
    private GameManager gameManager;
    private CharacterController2D characterController;

    public float lastHorizontalMove = 0f;
    public float horizontalMove = 0f;
    private float runSpeed = 15f; //Скорость перемещения игрока
    private bool jump = false;
    
    



    private void Start()
    {
        fistCollider.enabled = false;
        purchaseCollider.enabled = false;
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        characterController = GetComponent<CharacterController2D>();

    }

    //Считывание кнопок нажатых игроком
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if (horizontalMove != 0)
        {
            lastHorizontalMove = horizontalMove;
        }
        
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Restart"))
        {
            gameManager.RestartGame();
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Punch") && characterController.grounded)
        {
            audioManager.PlaySFX(punchSFX, 1.5f);

            fistCollider.enabled = true;
            animator.SetBool("IsPunching", true);
        }

        if (Input.GetButtonUp("Punch"))
        {
            fistCollider.enabled = false;
            animator.SetBool("IsPunching", false);
        }

        if (Input.GetButtonDown("Purchase"))
        {
            purchaseCollider.enabled = true;
        }

        if (Input.GetButtonUp("Purchase"))
        {
            purchaseCollider.enabled = false;
        }
    }

    //Передвижение игрока
    private void FixedUpdate()
    {
        if (enabled)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }
    }

}