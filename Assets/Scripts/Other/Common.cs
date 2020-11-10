using System.Collections;
using UnityEngine;

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
			//StartCoroutine(Delay());
			flipping = false;
		}
	}

	//Задержка
	public IEnumerator Delay()
	{
		yield return new WaitForSeconds(0.2f);
		flipping = false;
	}

}
