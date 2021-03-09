using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
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

            text.font = TextLocalizer.CurrentFont;

            text.fontSize = TextLocalizer.CurrentFontSize;

            text.text = TextLocalizer.ResolveStringValue("menu_" + text.text);

        }
    }

    public void MainMenu()
    {
        Common.MainMenu();
    }
}
