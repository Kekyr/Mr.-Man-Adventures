using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Common : MonoBehaviour
{
	private bool flipping = false;
	
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

	public static void AudioButtonSwitch(GameObject[] audioButtons, bool selector)
	{
		foreach (var audioButton in audioButtons)
		{
			audioButton.GetComponent<Image>().enabled = selector;
			audioButton.GetComponentInChildren<TextMeshProUGUI>().enabled = selector;
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






}
