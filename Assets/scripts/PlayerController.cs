using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float pushForce = 10f; // Сила, с которой персонаж толкает объекты

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Управление движением персонажа с помощью стрелок
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Если персонаж столкнулся с объектом, который имеет Rigidbody2D
        if (collision.gameObject.CompareTag("ReflectiveCube"))
        {
            Rigidbody2D cubeRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (cubeRb != null)
            {
                // Толкание куба, если персонаж с ним сталкивается
                Vector2 pushDirection = new Vector2(transform.position.x - collision.transform.position.x, 0).normalized;
                cubeRb.velocity = new Vector2(pushDirection.x * pushForce, cubeRb.velocity.y);
            }
        }
    }
}

