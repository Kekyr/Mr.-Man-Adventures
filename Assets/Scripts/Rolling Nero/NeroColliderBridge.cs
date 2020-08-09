using UnityEngine;

public class NeroColliderBridge : MonoBehaviour
{
    private NeroColliderListener _listener;
    public void Initialize(NeroColliderListener l)
    {
        _listener = l;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _listener.currentTag = this.gameObject.tag;
        _listener.OnCollisionEnter2D(collision);
    }
 
}
