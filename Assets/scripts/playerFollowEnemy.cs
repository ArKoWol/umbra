using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f; // Скорость движения врага
    public float visionRange = 15f; // Радиус видимости (увеличен для тестирования)
    public string playerTag = "Player"; // Тег игрока
    public string obstacleTag = "Platform"; // Тег препятствия
    public string enemyTag = "enemy"; // Тег противника

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Отключаем гравитацию
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero; // Останавливаем врага, если игрока не видно
        }
    }

    bool CanSeePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > visionRange)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionRange);

        while (hit.collider != null)
        {
            if (hit.collider.CompareTag(playerTag))
                return true;
            else if (hit.collider.CompareTag(obstacleTag))
                return false;
            else if (hit.collider.CompareTag(enemyTag))
            {
                hit = Physics2D.Raycast(hit.point + direction * 0.01f, direction, visionRange - hit.distance);
                continue;
            }
            break;
        }

        return false;
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}