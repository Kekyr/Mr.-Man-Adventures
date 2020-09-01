using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Texture2D alive;

    public Texture2D dead;

    private RawImage rawImage;

    private bool isAlive = false;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    public void ChangeSprite()
    {
        if (!isAlive)
        {
            rawImage.texture = dead;
            isAlive = true;
        }
        else
        {
            rawImage.texture = alive;
        }
    }
}
