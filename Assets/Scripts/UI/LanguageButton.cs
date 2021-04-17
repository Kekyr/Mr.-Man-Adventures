using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{

    public static bool languageIsRussian;
    
    public Sprite russian;
    public Sprite english;
    private Image image;


    private void Awake()
    {
        if (PlayerPrefs.GetString("Language") != "")
        {
            if (PlayerPrefs.GetString("Language") == "russian")
            {
                languageIsRussian = true;
            }
            else
            {
                languageIsRussian = false;
            }
        }
    }


    private void Start()
    {
        image = GetComponent<Image>();     

        if (languageIsRussian)
        {
            image.sprite = russian;
        }
        else
        {
            image.sprite = english;
        }
    }

    public void LanguageSwitch()
    {
        if(languageIsRussian)
        {
            image.sprite = english;

            TextLocalizer.CurrentLanguage = "english";
            PlayerPrefs.SetString("Language", TextLocalizer.CurrentLanguage);

            if (SceneManager.GetActiveScene().name=="Menu")
            {
                Menu.changeLanguage = true;
            }
            else if(SceneManager.GetActiveScene().name=="Level 1")
            {
                PauseMenu.changeLanguage = true;
            }

            languageIsRussian = false;

            TextLocalizer.CurrentFontSize = 120;
        }
        else
        {
            image.sprite = russian;

            TextLocalizer.CurrentLanguage = "russian";
            PlayerPrefs.SetString("Language", TextLocalizer.CurrentLanguage);

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                Menu.changeLanguage = true;
            }
            else if (SceneManager.GetActiveScene().name == "Level 1")
            {
                PauseMenu.changeLanguage = true;
            }

            languageIsRussian = true;
            
            if (SceneManager.GetActiveScene().name!="Menu" && SceneManager.GetActiveScene().name != "Introduction" && SceneManager.GetActiveScene().name != "Tutorial")
            {
                TextLocalizer.CurrentFontSize = 73;
            }
            else
            {
                TextLocalizer.CurrentFontSize = 85;
            }
        }
    }

}
