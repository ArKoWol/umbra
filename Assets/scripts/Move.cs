using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    public float speed = 5f;      // Скорость движения
    public float jumpForce = 10f; // Сила прыжка
    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer mSpriteRenderer; // Для изменения спрайта

    // Для отслеживания состояния гравитации
    private bool mIsGravityUp = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>(); // Получаем ссылку на SpriteRenderer
    }

    void Update()
    {
        // Движение влево-вправо, игнорируя вертикальную скорость
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Поворот спрайта в сторону движения
        if (moveInput != 0)
        {
            // Если двигаемся вправо, спрайт не меняет ориентацию (он смотрит вправо), если влево — инвертируется
            mSpriteRenderer.flipX = moveInput < 0;
        }

        // Прыжок по нажатию кнопки Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Проверка смены гравитации
        if (Input.GetKeyDown(KeyCode.X)) // Вы можете изменить эту клавишу
        {
            ToggleGravity();
        }
    }

    // Функция для прыжка
    private void Jump()
    {
        // Если гравитация инвертирована, прыжок будет в зависимости от направления
        float jumpDirection = mIsGravityUp ? -1 : 1;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpDirection);
    }

    // Переключение гравитации
    private void ToggleGravity()
    {
        mIsGravityUp = !mIsGravityUp;
        // Включаем инвертированную гравитацию
        Physics2D.gravity = mIsGravityUp ? Vector2.up * 9.81f : Vector2.down * 9.81f;
    }

    // Проверяем, находится ли персонаж на платформе
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
