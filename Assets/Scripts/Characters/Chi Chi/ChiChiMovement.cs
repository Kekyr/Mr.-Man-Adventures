using UnityEngine;
using Pathfinding;
public class ChiChiMovement: MonoBehaviour
{
    public Transform target;

    private Seeker seeker;
    private Rigidbody2D rigidBody2D;
    private Common common;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing1 = .2f;
    public float speed1 = 200f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing2 = .05f;
    public float speed2 = 4.5f;
    public float nextWaypointDistance = 3f;
    public bool isPlayerHere = false;
    public bool stopped = false;

    private Path path;
    private int currentWaypoint = 0;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetVelocity;
    private Vector2 destination;
    private bool facingRight = false;
    

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
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
            if(target==null)
            {
                target = FindObjectOfType<PlayerMovement>().transform;
            }
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
        if (isPlayerHere)
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody2D.position).normalized;
            targetVelocity = direction * speed1 * Time.fixedDeltaTime;

            rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing1);

            float distance = Vector2.Distance(rigidBody2D.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            
        }
        else
        {
            if (stopped)
            {
                speed2 *= -1;
                stopped = false;
            }

            targetVelocity = new Vector2(speed2, rigidBody2D.velocity.y);

            rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing2);

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
