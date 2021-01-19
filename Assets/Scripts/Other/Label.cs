using UnityEngine;

public class Label : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetTrigger("Flick");
    }
}
