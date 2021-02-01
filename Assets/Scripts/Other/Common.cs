using UnityEngine;
using UnityEngine.SceneManagement;

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

	


	

}
