using UnityEngine;

public class ChiChiColliderBridge : MonoBehaviour
{
    private ChiChiColliderListener _listener; //Родитель всех gameobject
    public void Initialize(ChiChiColliderListener l)
    {
        _listener = l;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        _listener.currentTag = this.gameObject.tag;
        _listener.OnTriggerEnter2D(collider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _listener.currentTag = this.gameObject.tag;
        _listener.OnCollisionEnter2D(collision);
    }
}
