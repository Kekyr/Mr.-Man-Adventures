using UnityEngine;

public class FoodColliderBridge : MonoBehaviour
{
    private FoodColliderListener _listener; //Родитель всех gameobject
    public void Initialize(FoodColliderListener l)
    {
        _listener = l;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        _listener.currentTag = this.gameObject.tag;
        _listener.currentGameObject = this.gameObject;
        _listener.OnTriggerEnter2D(collider);
    }
}
