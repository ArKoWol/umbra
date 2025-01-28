using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToMove : MonoBehaviour
{
    public float speed = 5f;        // Скорость движения
    public float jumpForce = 10f;  // Сила прыжка
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isRunning = false;
    private bool isGravityUp = false;
    private float gravity = 10f; // Adjust if needed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck не назначен! Убедитесь, что указали его в инспекторе.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleGravityToggle();
        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = CheckIfGrounded();

        // Сбрасываем флаг прыжка, если персонаж приземлился
        if (isGrounded && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJumping = false;
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Обновляем флаг бега
        isRunning = moveInput != 0;

        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float jumpDirection = isGravityUp ? -1 : 1;

            // Устанавливаем вертикальную скорость с учетом направления гравитации
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpDirection);
            isJumping = true; // Устанавливаем флаг прыжка
        }
    }

    private bool CheckIfGrounded()
    {
        // Проверяем, находится ли персонаж на земле с учетом слоя
        Vector2 checkPosition = groundCheck.position;
        Collider2D hit = Physics2D.OverlapCircle(checkPosition, groundCheckRadius, groundLayer);

        // Возвращаем true только если есть контакт с землей
        return hit != null;
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

        // Меняем направление гравитации
        Physics2D.gravity = isGravityUp ? Vector2.up * gravity : Vector2.down * gravity;
        rb.gravityScale = 1; // Gravity scale should always be 1 for proper physics

        // Переворачиваем спрайт персонажа
        spriteRenderer.flipY = isGravityUp;

        // Обновляем положение groundCheck, чтобы оно соответствовало новому направлению гравитации
        float offset = isGravityUp ? -Mathf.Abs(groundCheck.localPosition.y) : Mathf.Abs(groundCheck.localPosition.y);
        groundCheck.localPosition = new Vector3(0, offset, 0);

        // Обновляем флаг isGrounded после инверсии гравитации
        isGrounded = CheckIfGrounded();
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isRunning", isRunning); // Обновляем параметр "isRunning"
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}