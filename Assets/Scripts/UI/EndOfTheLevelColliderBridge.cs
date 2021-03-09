using UnityEngine;

public class EndOfTheLevelColliderBridge : MonoBehaviour
{
    private EndOfTheLevelColliderListener _listener; //Родитель всех gameobject
    public void Initialize(EndOfTheLevelColliderListener l)
    {
        _listener = l;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        _listener.currentTag = this.gameObject.tag;
        _listener.OnTriggerEnter2D(collider);
    }
}
