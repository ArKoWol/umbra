using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToMove : MonoBehaviour
{
    public float speed = 5f;        // Скорость движения
    public float jumpForce = 10f;   // Сила прыжка

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isJumping = false;
    private bool isRunning = false;
    private bool isOnPlatform = false; // Проверка, находится ли на платформе

    private GravityShift gravityShift;

    void Start()
    {
        // Получаем компоненты
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gravityShift = GetComponent<GravityShift>();

        // Проверяем, что компоненты найдены
        if (rb == null) Debug.LogError("Rigidbody2D не найден на объекте: " + gameObject.name);
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer не найден на объекте: " + gameObject.name);
        if (animator == null) Debug.LogError("Animator не найден на объекте: " + gameObject.name);
        if (gravityShift == null) Debug.LogError("GravityShift не найден на объекте: " + gameObject.name);
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        UpdateAnimatorParameters();
    }

    // Обработка движения
    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        isRunning = moveInput != 0;

        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    // Обработка прыжка
    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnPlatform && !isJumping)
        {
            float jumpDirection = (gravityShift != null && gravityShift.mIsGravityUp) ? -1 : 1;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpDirection);
            isJumping = true;
            isOnPlatform = false;
        }
    }

    // Обновление параметров аниматора
    private void UpdateAnimatorParameters()
    {
        if (animator != null)
        {
            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isRunning", isRunning);
        }
    }

    // Обработка столкновений с платформой
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
        }
    }
}