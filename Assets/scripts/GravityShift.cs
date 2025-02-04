using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    private Rigidbody2D mRb;

    private float mGravity = 9.81f * 2.0f; // adjust if needed
    
    private Camera mCamera;

    protected bool mIsGravityUp = false;

    // Start is called before the first frame update
    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
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

    protected bool IsToggleGravity()
    {
        if (Input.GetKeyDown(KeyCode.R))
            return true;

        // Later check for Joy Stick

        return false;
    }

    protected void ToggleGravity(){
        mIsGravityUp = !mIsGravityUp;
    }

    protected void SetGravity(){
        if (mIsGravityUp) {
            Physics2D.gravity = Vector2.up * mGravity;
        }
        else {
            Physics2D.gravity = Vector2.down * mGravity;
        }
    }
}
