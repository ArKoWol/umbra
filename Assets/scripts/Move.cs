using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость передвижения
    public float jumpForce = 10f; // Сила прыжка
    public float fallMultiplier = 2.5f; // Множитель ускорения при падении
    public float lowJumpMultiplier = 2f; // Множитель для более низкого прыжка
    public float fallSpeed = 5f; // Максимальная скорость падения при зажатом пробеле
    private bool isGrounded; // Флаг для проверки, на земле ли игрок

    public Vector2 areaSize = new Vector2(3f, 2f); // Размер области, в которой камера не двигается
    public float followSpeed = 2f; // Начальная скорость движения камеры
    public float acceleration = 1f; // Ускорение движения камеры
    private Vector3 velocity = Vector3.zero; // Начальная скорость камеры
    private Camera mainCamera;

    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        col = GetComponent<Collider2D>(); // Получаем компонент Collider2D
        mainCamera = Camera.main; // Получаем ссылку на основную камеру
    }

    void Update()
    {
        // Проверка, на земле ли игрок
        isGrounded = Physics2D.IsTouchingLayers(col, LayerMask.GetMask("platform dm1", "platform dm2", "dm1-2"));

        // Движение игрока
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Отзеркаливание персонажа в зависимости от направления движения
        if (moveX != 0) // Если движение влево или вправо
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1f, 1f); // Отзеркаливаем персонажа
        }

        // Прыжок
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Плавное падение
        if (rb.velocity.y < 0)
        {
            if (Input.GetKey(KeyCode.Space)) // Если пробел зажат
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -fallSpeed)); // Устанавливаем максимальную скорость падения
            }
            else
            {
                rb.gravityScale = fallMultiplier; // Если пробел не зажат, ускоряем падение
            }
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) // Если не зажат пробел и идет вверх
        {
            rb.gravityScale = lowJumpMultiplier; // Сниженная гравитация для короткого прыжка
        }
        else
        {
            rb.gravityScale = 1f; // Стандартная гравитация
        }

        // Логика для плавного следования камеры с замедлением в области
        Vector3 targetPosition = transform.position;
        Vector3 cameraPosition = mainCamera.transform.position;

        // Определяем область, в которой камера не двигается
        Vector3 minBounds = targetPosition - new Vector3(areaSize.x, areaSize.y, 0f);
        Vector3 maxBounds = targetPosition + new Vector3(areaSize.x, areaSize.y, 0f);

        // Если игрок в пределах области, камера не двигается
        if (cameraPosition.x > minBounds.x && cameraPosition.x < maxBounds.x &&
            cameraPosition.y > minBounds.y && cameraPosition.y < maxBounds.y)
        {
            return; // Камера не двигается, если игрок в пределах области
        }

        // Если игрок выходит за пределы области, камера начинает двигаться
        Vector3 direction = targetPosition - cameraPosition;

        // Постепенное ускорение камеры
        float smoothSpeed = followSpeed + acceleration * Time.deltaTime;
        mainCamera.transform.position = Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, smoothSpeed);
    }
}
