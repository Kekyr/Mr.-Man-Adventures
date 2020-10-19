using UnityEngine;

public class EndOfTheLevel : MonoBehaviour
{
    public MovingCamera movingCamera;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        movingCamera.enabled = false;
    }
}
