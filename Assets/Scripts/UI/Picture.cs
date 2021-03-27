using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }
}
