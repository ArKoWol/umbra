using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversionShift : MonoBehaviour
{
    public Sprite characterStartSprite;
    public Sprite characterNewSprite;

    public GameObject background;
    public Sprite backgroundStartSprite;
    public Sprite backgroundNewSprite;

    // Списки для хранения платформ
    private List<GameObject> platformsStart = new List<GameObject>();
    private List<GameObject> platformsNew = new List<GameObject>();

    private bool isStartState = true;
    private bool isGrounded = false; // Проверка того, что персонаж на платформе
    private bool isGravityUp = false; // Флаг для переключения гравитации

    private Rigidbody2D rb;
    private Animator animator;

    public float jumpForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found! Ensure an Animator component is attached.");
        }

        // Заполняем списки платформ
        platformsStart.AddRange(GameObject.FindGameObjectsWithTag("Platform"));
        platformsNew.AddRange(GameObject.FindGameObjectsWithTag("NewPlatform"));

        // Устанавливаем начальное состояние
        SetStartState();
    }

    void Update()
{
    bool wasJumping = animator?.GetBool("isJumping") ?? false;

    // Діагностика: Логи для відстеження стану
    Debug.Log($"Update: isGrounded = {isGrounded}, isJumping = {wasJumping}");

    if (!wasJumping && !isGrounded)
    {
        Debug.Log("Set isJumping = true");
        animator?.SetBool("isJumping", true);
    }
    else if (wasJumping && isGrounded)
    {
        Debug.Log("Set isJumping = false");
        animator?.SetBool("isJumping", false);
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
        isStartState = !isStartState;
        if (isStartState)
        {
            SetStartState();
        }
        else
        {
            SetNewState();
        }
    }

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        Jump();
    }

    if (Input.GetKeyDown(KeyCode.X))
    {
        ToggleGravity();
    }
}


    void SetStartState()
    {
        Debug.Log("Switching to Start State");

        // Устанавливаем начальный фон
        background.GetComponent<SpriteRenderer>().sprite = backgroundStartSprite;

        // Устанавливаем спрайт персонажа
        GetComponent<SpriteRenderer>().sprite = characterStartSprite;

        // Активируем начальные платформы и скрываем новые
        SetPlatformsActive(platformsStart, true);
        SetPlatformsActive(platformsNew, false);
    }

    void SetNewState()
    {
        Debug.Log("Switching to New State");

        // Устанавливаем новый фон
        background.GetComponent<SpriteRenderer>().sprite = backgroundNewSprite;

        // Меняем спрайт персонажа
        GetComponent<SpriteRenderer>().sprite = characterNewSprite;

        // Скрываем начальные платформы и активируем новые
        SetPlatformsActive(platformsStart, false);
        SetPlatformsActive(platformsNew, true);
    }

    void SetPlatformsActive(List<GameObject> platforms, bool isActive)
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(isActive);

            // Принудительное обновление физики платформы
            var collider = platform.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false; // Отключаем
                collider.enabled = true;  // Включаем заново
            }

            Debug.Log($"{platform.name} set active: {isActive}");
        }
    }

    void Jump()
    {
        if (!isGrounded) return; // Перешкоджаємо повторному стрибку

        float jumpDirection = isGravityUp ? -1 : 1;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpDirection);

        animator?.SetBool("isJumping", true); // Встановлюємо анімацію стрибка
    }

    void ToggleGravity()
    {
        isGravityUp = !isGravityUp;
        Physics2D.gravity = isGravityUp ? Vector2.up * 9.81f : Vector2.down * 9.81f;
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    if ((collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("NewPlatform")) && !isGrounded)
    {
        Debug.Log($"Grounded on {collision.gameObject.name}");
        isGrounded = true;
        animator?.SetBool("isJumping", false);
    }
}

void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("NewPlatform"))
    {
        Debug.Log($"Left platform {collision.gameObject.name}");
        isGrounded = false;
    }
}

}