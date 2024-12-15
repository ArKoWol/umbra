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

    void Start()
    {
        // Заполняем списки платформ
        platformsStart.AddRange(GameObject.FindGameObjectsWithTag("Platform"));
        platformsNew.AddRange(GameObject.FindGameObjectsWithTag("NewPlatform"));

        // Устанавливаем начальное состояние
        SetStartState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Переключаем состояние
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

        // Управление прыжком
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float jumpForce = 10f;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        // Проверяем наличие компонента Collider2D и тегов платформ
        if (collision.gameObject.TryGetComponent(out Collider2D collider) &&
            (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("NewPlatform")))
        {
            isGrounded = true;
            Debug.Log("Player is now grounded.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"Player left platform {collision.gameObject.name}");

        if (collision.gameObject.TryGetComponent(out Collider2D collider) &&
            (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("NewPlatform")))
        {
            isGrounded = false;
        }
    }
}
