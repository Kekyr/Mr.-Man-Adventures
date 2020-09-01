using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Collider2D fistCollider;//Коллайдер кулака, включается после удара и выключается по его окончании

    [SerializeField] private float runSpeed = 15f; //Скорость перемещения игрока
    public float horizontalMove = 0f;
    private bool jump = false;


    private void Start()
    {
        fistCollider.enabled = false;
    }

    //Считывание кнопок нажатых игроком
    private void Update()
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

    //Отключение анимации прыжка после приземления
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
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