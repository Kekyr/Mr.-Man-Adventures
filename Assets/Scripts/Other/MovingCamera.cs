using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform target;
    public Transform backgrounds;

    public float smoothTime = 0.3f;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 backgroundVelocity = Vector3.zero;
    private Vector3 CameraNewPosition;
    private Vector3 BackgroundNewPosition;
    private float pastXPosition = 6;
    

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            CameraNewPosition = target.TransformPoint(new Vector3(0, 0, -1));
            BackgroundNewPosition = target.TransformPoint(new Vector3(0, 0, 1));

            BackgroundNewPosition.y = 0f;
            CameraNewPosition.y = 3.54f;
            BackgroundNewPosition.z = 0;

            if (((CameraNewPosition.x > pastXPosition) && (BackgroundNewPosition.x > pastXPosition)))
            {
                transform.position = Vector3.SmoothDamp(transform.position, CameraNewPosition, ref cameraVelocity, smoothTime);

                backgrounds.position = Vector3.SmoothDamp(backgrounds.position, BackgroundNewPosition, ref backgroundVelocity, smoothTime);
              
                pastXPosition = target.position.x;
            }
        }
    }
}
