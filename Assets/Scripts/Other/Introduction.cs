using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public Sprite[] sprites;
    private TextMeshProUGUI text;
    private Picture[] pictures;
    private Picture tutorialPicture1;
    private Picture tutorialPicture2;
    private Picture cutscenePicture;
    public string[] texts;
    private int currentPicture;
    private int currentText;

    private void Awake()
    {
        text = FindObjectOfType<TextMeshProUGUI>();
        pictures = FindObjectsOfType<Picture>();

        foreach (var picture in pictures)
        {
            if (picture.CompareTag("FirstPicture"))
            {
                tutorialPicture1 = picture;
            }
            else if(picture.CompareTag("SecondPicture"))
            {
                tutorialPicture2 = picture;
            }
            else
            {
                cutscenePicture = picture;
            }
        }

        for(var i=0; i<texts.Length; i++)
        {
            texts[i]= TextLocalizer.ResolveStringValue(i);
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Continue"))
        {
            Continue();
        }
    }

    public void Continue()
    {
        if (tutorialPicture1 != null && tutorialPicture2 != null)
        {
            if (currentPicture != sprites.Length)
            {
                tutorialPicture1.spriteRenderer.sprite = sprites[currentPicture];
                tutorialPicture2.spriteRenderer.sprite = sprites[currentPicture + 1];
            }
            else
            {
                LoadNextScene();
            }

            if (currentText != texts.Length)
            {
                text.text = texts[currentText];
            }

            currentPicture += 2;
            currentText++;
        }
        else
        {
            if (currentPicture != sprites.Length)
            {
                cutscenePicture.spriteRenderer.sprite = sprites[currentPicture];
            }
            else
            {
                LoadNextScene();
            }

            if (currentText != texts.Length)
            {
                text.text = texts[currentText];
            }

            currentPicture++;
            currentText++;
        }
    }

    private void LoadNextScene()
    {
        Common.LoadNextScene();
    }



}
