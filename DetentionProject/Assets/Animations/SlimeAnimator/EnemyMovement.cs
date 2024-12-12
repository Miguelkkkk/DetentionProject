using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float obstacleAvoidanceDistance = 1.5f;
    private bool isFollowing = false;
    private Rigidbody2D rb;
    private Animator _animator;


    public EnemyHealth health;

    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health != null && health.isDeafeated())
        {

            rb.velocity = Vector2.zero;
            return;
        }
        _animator.SetBool("IsMoving", isFollowing);
        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 avoidanceDirection = AvoidObstacles(directionToPlayer);

        Vector2 movement = avoidanceDirection * followSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    Vector2 AvoidObstacles(Vector2 direction)
    {
        // Cast múltiplos raycasts em diferentes direções ao redor do inimigo
        Vector2[] directions = {
            direction,
            Quaternion.Euler(0, 0, 45) * direction,
            Quaternion.Euler(0, 0, -45) * direction
        };

        Vector2 adjustedDirection = direction;
        foreach (Vector2 dir in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, obstacleAvoidanceDistance);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                Vector2 hitNormal = hit.normal;
                adjustedDirection += hitNormal * 0.5f; // Ajuste suave na direção
            }
        }

        adjustedDirection.Normalize();
        return adjustedDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }
}
