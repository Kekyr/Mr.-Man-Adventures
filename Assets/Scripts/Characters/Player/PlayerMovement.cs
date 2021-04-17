using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip punchSFX;
    public CharacterController2D controller;
    public Animator animator;
    public Collider2D fistCollider;//Коллайдер кулака, включается после удара и выключается по его окончании
    public Collider2D purchaseCollider;
    private AudioManager audioManager;
    private CharacterController2D characterController;
    private Button button;



    public float lastHorizontalMove = 0f;
    public float horizontalMove = 0f;
    private float runSpeed = 15f; //Скорость перемещения игрока
    private bool jump = false;
    private bool isButtonPressed = false;


    private void Start()
    {
        fistCollider.enabled = false;
        purchaseCollider.enabled = false;
        audioManager = AudioManager.instance;
        characterController = GetComponent<CharacterController2D>();
        GameManager.instance.startMusic = true;

    }

    //Считывание кнопок нажатых игроком
    private void Update()
    {
        #if UNITY_ANDROID
        
        if(isButtonPressed)
        {
            if(button.CompareTag("Left Button"))
            {
                horizontalMove = -runSpeed;
            }
            else
            {
                horizontalMove = runSpeed;
            }
        }
        else
        {
            horizontalMove = 0f;
        }

        #else
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
       
        #endif

        if (horizontalMove != 0)
        {
            lastHorizontalMove = horizontalMove;
        }
        
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
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

    #if UNITY_ANDROID
    
    public void Jump()
    {
        jump = true;
    }


    public void Purchase()
    {
        purchaseCollider.enabled = true;
        StartCoroutine(PurchaseDelay());
        
    }

    private IEnumerator PurchaseDelay()
    {
        yield return new WaitForSeconds(0.5f);
        purchaseCollider.enabled = false;
    }


    public void onPointerUpPunchButton()
    {
        fistCollider.enabled = false;
        animator.SetBool("IsPunching", false);

    }

    public void onPointerDownPunchButton()
    {
        if (characterController.grounded)
        {
            audioManager.PlaySFX(punchSFX, 1.5f);

            fistCollider.enabled = true;
            animator.SetBool("IsPunching", true);


        }
    }

    public void onPointerUpRunButton(Button key)
    {
        button = key;
        isButtonPressed = false;
    }

    public void onPointerDownRunButton(Button key)
    {
        button = key;
        isButtonPressed = true;
    }

    public void onPointerUpJumpButton()
    {
        jump = false;

    }

    public void onPointerDownJumpButton()
    {
        jump = true;

    }



#endif

}