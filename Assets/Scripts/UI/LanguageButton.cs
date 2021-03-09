using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{

    public static bool languageIsRussian;
    
    public Sprite russian;
    public Sprite english;
    private Image image;

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

            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                Menu.changeLanguage = true;
            }
            else if(SceneManager.GetActiveScene().buildIndex==3)
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

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Menu.changeLanguage = true;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                PauseMenu.changeLanguage = true;
            }

            languageIsRussian = true;
            
            if (SceneManager.GetActiveScene().buildIndex >= 3)
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
