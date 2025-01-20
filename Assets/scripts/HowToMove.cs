using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToMove : MonoBehaviour
{
    public float speed = 5f;      // Скорость движения
    public float jumpForce = 10f; // Сила прыжка

    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer mSpriteRenderer; // Для изменения спрайта
    private Animator animator; // Аниматор для управления анимацией

    // Для отслеживания состояния гравитации
    private bool mIsGravityUp = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>(); // Получаем ссылку на SpriteRenderer
        animator = GetComponent<Animator>(); // Получаем ссылку на Animator

        // Проверка, установлен ли Animator
        if (animator == null)
        {
            Debug.LogError("Animator не найден! Проверьте, прикреплен ли компонент Animator.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleGravityToggle();
    }

    private void HandleMovement()
    {
        // Движение влево-вправо
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Поворот спрайта в сторону движения
        if (moveInput != 0)
        {
            mSpriteRenderer.flipX = moveInput < 0;
            animator?.SetBool("isRunning", true); // Проверка на null для безопасности
        }
        else
        {
            animator?.SetBool("isRunning", false);
        }
    }

    private void HandleJumping()
    {
        // Прыжок по нажатию кнопки Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Устанавливаем анимацию прыжка в зависимости от состояния
        animator?.SetBool("isJumping", !isGrounded);
    }

    private void Jump()
    {
        float jumpDirection = mIsGravityUp ? -1 : 1;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpDirection);
    }

    private void HandleGravityToggle()
    {
        // Переключение гравитации
        if (Input.GetKeyDown(KeyCode.X)) // Можно изменить клавишу на любую другую
        {
            ToggleGravity();
        }
    }

    private void ToggleGravity()
    {
        mIsGravityUp = !mIsGravityUp;
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
