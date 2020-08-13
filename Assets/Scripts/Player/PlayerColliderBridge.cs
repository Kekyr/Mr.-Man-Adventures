using UnityEngine;

public class PlayerColliderBridge : MonoBehaviour
{
    PlayerColliderListener _listener; //Родитель всех gameobject
    public void Initialize(PlayerColliderListener l)
    {
        _listener = l;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _listener.OnTriggerEnter2D(collision);
    }
}
