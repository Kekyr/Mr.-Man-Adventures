using UnityEngine;

public class Label : MonoBehaviour
{
    public Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();

        if (PlayerPrefs.GetString("Language") == "russian")
        {
            animator.enabled = false;
            animator.SetBool("isRussian", true);
            animator.enabled = true;
        }
    }
    
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(gameObject);
        }
        else if(Menu.fromGame)
        {
            Menu.fromGame = false;
            Destroy(gameObject);
        }

    }

    private void OnDisable()
    {
        //PlayerPrefs.DeleteAll();
        FindObjectOfType<Menu>().startMenu = true;
    }
 
}
