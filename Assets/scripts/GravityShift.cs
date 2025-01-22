using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    private Rigidbody2D mRb;
    private SpriteRenderer mSpriteRenderer; // ��������� ���������� ��� SpriteRenderer

    private float mGravity = 5f * 2.0f; // adjust if needed

    private Camera mCamera;

    private bool mIsGravityUp = false;

    // Start is called before the first frame update
    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mSpriteRenderer = GetComponent<SpriteRenderer>(); // ������������� SpriteRenderer
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
        if (Input.GetKeyDown(KeyCode.R))
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
            // �������������� ������
            mSpriteRenderer.flipY = true;
        }
        else
        {
            Physics2D.gravity = Vector2.down * mGravity;
            // ��������������� ���������� ��� �������
            mSpriteRenderer.flipY = false;
        }
    }
}
