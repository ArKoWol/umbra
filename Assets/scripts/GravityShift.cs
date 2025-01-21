using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    private Rigidbody2D mRb;
    private SpriteRenderer mSpriteRenderer; // Добавляем переменную для SpriteRenderer

    private float mGravity = 5f * 2.0f; // adjust if needed

    private Camera mCamera;

    private bool mIsGravityUp = false;

    // Start is called before the first frame update
    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>(); // Инициализация SpriteRenderer
        mCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsToggleGravity())
        {
            ToggleGravity();
        }

        SetGravity();
    }

    private bool IsToggleGravity()
    {
        if (Input.GetKeyDown(KeyCode.X))
            return true;

        // Later check for Joy Stick

        return false;
    }

    private void ToggleGravity()
    {
        mIsGravityUp = !mIsGravityUp;
    }

    private void SetGravity()
    {
        if (mIsGravityUp)
        {
            Physics2D.gravity = Vector2.up * mGravity;
            // Переворачиваем спрайт
            mSpriteRenderer.flipY = true;
        }
        else
        {
            Physics2D.gravity = Vector2.down * mGravity;
            // Восстанавливаем нормальный вид спрайта
            mSpriteRenderer.flipY = false;
        }
    }
}
