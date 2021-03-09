using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameObject[] buttons;
    private TextMeshProUGUI text;

    private void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        ChangeText(buttons);
    }

    private void ChangeText(GameObject[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            text = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

            //Debug.Log("text:" + text.text);

            text.font = TextLocalizer.CurrentFont;
            
            if (text.text == "gameover")
            {
                Debug.Log("I'm here1");
                if (TextLocalizer.CurrentLanguage=="russian")
                {
                    text.fontSize = 120;
                }
                else
                {
                    Debug.Log("I'm here2");
                    text.fontSize = 200;
                }
            }
            else
            {
                text.fontSize = TextLocalizer.CurrentFontSize;
            }

            //Debug.Log("id:" + "menu_" + text.text);
            text.text = TextLocalizer.ResolveStringValue("menu_" + text.text);

        }
    }

    public void Restart()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 12, false);
        Physics2D.IgnoreLayerCollision(8, 0, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenu()
    {
        Common.MainMenu();
    }
}
