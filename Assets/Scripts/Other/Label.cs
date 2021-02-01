using UnityEngine;

public class Label : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        FindObjectOfType<Menu>().startMenu = true;
    }
 
}
