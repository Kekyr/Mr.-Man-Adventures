using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public Sprite[] sprites;
    private TextMeshProUGUI text;
    private Picture[] pictures;
    private Picture tutorialPicture1;
    private Picture tutorialPicture2;
    private Picture cutscenePicture;
    private string[] introductionTexts=new string[8];
    private string[] tutorialTexts = new string[5];
    private int currentPicture;
    private int currentText = 1;

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
            else if (picture.CompareTag("SecondPicture"))
            {
                tutorialPicture2 = picture;
            }
            else
            {
                cutscenePicture = picture;
            }
        }

        text.font = TextLocalizer.CurrentFont;

        if (TextLocalizer.CurrentLanguage == "russian")
        {
            text.fontSize = TextLocalizer.CurrentFontSize - 30;
        }
        else
        {
            text.fontSize = TextLocalizer.CurrentFontSize-40;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            for (var i = 0; i < introductionTexts.Length; i++)
            {
                introductionTexts[i] = TextLocalizer.ResolveStringValue("introduction_" + i);
            }

            text.text = introductionTexts[0];
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            currentPicture = 2;
            for (var i = 0; i < tutorialTexts.Length; i++)
            {
                tutorialTexts[i] = TextLocalizer.ResolveStringValue("tutorial_" + i);
            }

            text.text = tutorialTexts[0];
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

            if (currentText != tutorialTexts.Length)
            {
                text.text = tutorialTexts[currentText];
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

            if (currentText != introductionTexts.Length)
            {
                text.text = introductionTexts[currentText];
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
