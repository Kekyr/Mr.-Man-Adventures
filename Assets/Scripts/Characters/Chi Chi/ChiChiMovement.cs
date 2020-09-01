using UnityEngine;
using Pathfinding;
public class ChiChiMovement: MonoBehaviour
{
    public Transform target;

    private Seeker seeker;
    private Rigidbody2D rigidBody2D;
    private Common common;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .2f;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private Vector3 velocity = Vector3.zero;
    private Vector2 destination;
    private bool facingRight = false;

    private void Start()
    {
        common = FindObjectOfType<Common>();
        seeker = GetComponent<Seeker>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 12, true);

        InvokeRepeating("UpdatePath", 0f,.2f);
    }


    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            destination = target.position+new Vector3(1f,0);
            seeker.StartPath(rigidBody2D.position, destination, OnPathComplete);
        }
    }


    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if(currentWaypoint>=path.vectorPath.Count)
        {
            return;
        }
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody2D.position).normalized;
        Vector3 targetVelocity = direction * speed * Time.fixedDeltaTime;

        rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        float distance = Vector2.Distance(rigidBody2D.position, path.vectorPath[currentWaypoint]);

        if(distance<nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (targetVelocity.x >= 0.01f && !facingRight)
        {
            common.Flip(ref facingRight, gameObject);
        }
        else if (targetVelocity.x <= -0.01f && facingRight)
        {
            common.Flip(ref facingRight, gameObject);
        }
    }
}
