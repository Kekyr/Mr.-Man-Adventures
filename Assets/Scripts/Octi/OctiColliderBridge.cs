using UnityEngine;

public class OctiColliderBridge : MonoBehaviour
{
    private OctiColliderListener _listener; //Родитель всех gameobject
    public void Initialize(OctiColliderListener l)
    {
        _listener = l;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _listener.currentTag = this.gameObject.tag;
        _listener.OnCollisionEnter2D(collision);
    }
}
