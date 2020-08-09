using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderBridge : MonoBehaviour
{
    PlayerColliderListener _listener;
    public void Initialize(PlayerColliderListener l)
    {
        _listener = l;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _listener.OnTriggerEnter2D(collision);
    }
}
