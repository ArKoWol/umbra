using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToMove : MonoBehaviour
{
    public float speed = 5f;        // Скорость движения
    public float jumpForce = 10f;  // Сила прыжка

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isJumping = false;
    private bool isRunning = false;
    private bool isOnPlatform = false; // Проверка, находится ли на платформе

    private float gravity = 10f; // Гравитация
    public bool isGravityUp = false; // Проверка инверсии гравитации

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleGravityToggle();
        UpdateAnimatorParameters();
    }

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

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnPlatform)
        {
            float jumpDirection = isGravityUp ? -1 : 1; // Учитываем направление гравитации
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpDirection); // Используем корректное направление прыжка
            isJumping = true;
        }
    }

    private void HandleGravityToggle()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleGravity();
        }
    }

    private void ToggleGravity()
    {
        isGravityUp = !isGravityUp;

        Physics2D.gravity = isGravityUp ? Vector2.up * gravity : Vector2.down * gravity;
        rb.gravityScale = 1;

        spriteRenderer.flipY = isGravityUp;
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isRunning", isRunning);
    }

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
