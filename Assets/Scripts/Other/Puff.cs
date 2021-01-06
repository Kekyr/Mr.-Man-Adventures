using UnityEngine;

public class Puff : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public bool destruction = false;
    private int clipNumber;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (destruction)
        {
            animator.SetTrigger("Puff");
            clipNumber = Random.Range(0, audioClips.Length);
            audioManager.PlaySFX(audioClips[clipNumber]);
            destruction = false;
        }
    }
}
