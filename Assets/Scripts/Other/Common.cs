using UnityEngine;

public class Common : MonoBehaviour
{
	//Изменение направления спрайта
	public void Flip(ref bool facingRight, GameObject gameObject)
	{
		facingRight = !facingRight;

		Vector3 theScale = gameObject.transform.localScale;
		theScale.x *= -1;
		gameObject.transform.localScale = theScale;
	}
}
