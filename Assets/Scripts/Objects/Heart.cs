using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Texture2D alive;
    public Texture2D dead;
    private RawImage rawImage;

    private bool isDead = false;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    //Смена спрайта при потере одной жизни
    public void ChangeSprite()
    {
        if (!isDead)
        {
            rawImage.texture = dead;
            isDead = true;
        }
        else
        {
            if (rawImage != null)
            {
                rawImage.texture = alive;
            }
            isDead = false;
        }
    }
}
