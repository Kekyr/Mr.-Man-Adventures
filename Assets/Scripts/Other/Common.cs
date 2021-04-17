using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Common : MonoBehaviour
{
	private TextMeshProUGUI text;
	private bool flipping = false;
	private int currentId;
	
	//Изменение направления спрайта
	public void Flip(ref bool facingRight, GameObject gameObject)
	{
		if (!flipping)
		{
			flipping = true;
			facingRight = !facingRight;

			Vector3 theScale = gameObject.transform.localScale;
			theScale.x *= -1;
			gameObject.transform.localScale = theScale;
			flipping = false;
		}
	}

	public static void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static void SettingsButtonSwitch(GameObject[] settingsButtons, bool selector)
	{
		foreach (var settingsButton in settingsButtons)
		{
			settingsButton.GetComponent<Image>().enabled = selector;
			settingsButton.GetComponentInChildren<TextMeshProUGUI>().enabled = selector;
		}
	}

	public static void ButtonSwitch(GameObject[] buttons, bool selector)
	{
		foreach (var button in buttons)
		{
			button.GetComponent<Image>().enabled = selector;
			button.GetComponentInChildren<TextMeshProUGUI>().enabled = selector;
		}
	}


	public static void Quit()
	{
		Application.Quit();
	}

	public static void MainMenu()
	{
		Menu.fromGame = true;
		SceneManager.LoadScene(1);
	}

	public void ChangeText(GameObject[] buttons, bool changeLanguage, List<string> ids)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			text = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

			if (!changeLanguage)
			{
				ids.Add(text.text);
			}

			text.font = TextLocalizer.CurrentFont;

			text.fontSize = TextLocalizer.CurrentFontSize;

			if (!changeLanguage)
			{
				text.text = TextLocalizer.ResolveStringValue("menu_" + text.text);
			}
			else
			{
				text.text = TextLocalizer.ResolveStringValue("menu_" + ids[currentId]);
				currentId++;
				if (SceneManager.GetActiveScene().name == "Level 1")
				{
					if (currentId == ids.Count - 1)
					{
						currentId = 0;
					}
				}
				else
				{
					if (currentId == ids.Count)
					{
						currentId = 0;
					}
				}
			}
		}
	}








}
