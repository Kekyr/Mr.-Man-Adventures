using UnityEngine;

public class Label : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(gameObject);
        }
        else if(Menu.fromGame)
        {
            Menu.fromGame = false;
            Destroy(gameObject);
        }

    }

    private void OnDisable()
    {
        FindObjectOfType<Menu>().startMenu = true;
    }
 
}
