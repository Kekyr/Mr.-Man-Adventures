using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform target;

    private float smoothTime = 0.3f;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 CameraNewPosition;
    private float pastXPosition = -5.25f;
    

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            CameraNewPosition = target.TransformPoint(new Vector3(0, 0, -1));
            CameraNewPosition.y = 3.54f;

            if (CameraNewPosition.x > pastXPosition) 
            {
                transform.position = Vector3.SmoothDamp(transform.position, CameraNewPosition, ref cameraVelocity, smoothTime);
              
                pastXPosition = target.position.x;
            }
        }
    }
}
