using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject skip;
    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;
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
        text1 = GameObject.FindGameObjectWithTag("Text").GetComponent<TextMeshProUGUI>();
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

        if(PlayerPrefs.GetInt("Introduction")==1)
        {
            skip.SetActive(true);
            text2=skip.GetComponentInChildren<TextMeshProUGUI>();
            text2.font=TextLocalizer.CurrentFont;

            if (TextLocalizer.CurrentLanguage == "russian")
            {
                text2.fontSize = TextLocalizer.CurrentFontSize - 30;
            }
            else
            {
                text2.fontSize = TextLocalizer.CurrentFontSize - 40;
            }

            text2.text = TextLocalizer.ResolveStringValue("skip");

        }

        text1.font = TextLocalizer.CurrentFont;

        if (TextLocalizer.CurrentLanguage == "russian")
        {
            text1.fontSize = TextLocalizer.CurrentFontSize - 30;
        }
        else
        {
            text1.fontSize = TextLocalizer.CurrentFontSize-40;
        }

        if (SceneManager.GetActiveScene().name=="Introduction")
        {
            for (var i = 0; i < introductionTexts.Length; i++)
            {
                introductionTexts[i] = TextLocalizer.ResolveStringValue("introduction_" + i);
            }

            text1.text = introductionTexts[0];
        }
        else if(SceneManager.GetActiveScene().name =="Tutorial")
        {
            currentPicture = 2;
            for (var i = 0; i < tutorialTexts.Length; i++)
            {
                tutorialTexts[i] = TextLocalizer.ResolveStringValue("tutorial_" + i);
            }

            text1.text = tutorialTexts[0];
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
                tutorialPicture1.image.sprite = sprites[currentPicture];
                tutorialPicture2.image.sprite = sprites[currentPicture + 1];
            }
            else
            {
                LoadNextScene();
            }

            if (currentText != tutorialTexts.Length)
            {
                text1.text = tutorialTexts[currentText];
            }

            currentPicture += 2;
            currentText++;
        }
        else
        {
            if (currentPicture != sprites.Length)
            {
                cutscenePicture.image.sprite = sprites[currentPicture];
            }
            else
            {
                PlayerPrefs.SetInt("Introduction", 1);
                LoadNextScene();
            }

            if (currentText != introductionTexts.Length)
            {
                text1.text = introductionTexts[currentText];
            }

            currentPicture++;
            currentText++;
        }
    }

    public void LoadNextScene()
    {
        Common.LoadNextScene();
    }

}
