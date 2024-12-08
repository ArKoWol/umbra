using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    private Rigidbody2D mRb;

    private float mGravity = 9.81f * 2.0f; // adjust if needed
    
    private Camera mCamera;

    private bool mIsGravityUp = false;

    // Start is called before the first frame update
    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mCamera = Camera.main;
        
        GenerateCollidersAcrossScreen();        
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

    private void ToggleGravity(){
        mIsGravityUp = !mIsGravityUp;
    }

    private void SetGravity(){
        if (mIsGravityUp) {
            Physics2D.gravity = Vector2.up * mGravity;
        }
        else {
            Physics2D.gravity = Vector2.down * mGravity;
        }
    }

    void GenerateCollidersAcrossScreen()
        {
        Vector2 lDCorner = mCamera.ViewportToWorldPoint(new Vector3(0, 0f, mCamera.nearClipPlane));
        Vector2 rUCorner = mCamera.ViewportToWorldPoint(new Vector3(1f, 1f, mCamera.nearClipPlane));
        Vector2[] colliderpoints;

        EdgeCollider2D upperEdge = new GameObject("upperEdge").AddComponent<EdgeCollider2D>();
        colliderpoints = upperEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, rUCorner.y);
        upperEdge.points = colliderpoints;

        EdgeCollider2D lowerEdge = new GameObject("lowerEdge").AddComponent<EdgeCollider2D>();
        colliderpoints = lowerEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
        lowerEdge.points = colliderpoints;

        EdgeCollider2D leftEdge = new GameObject("leftEdge").AddComponent<EdgeCollider2D>();
        colliderpoints = leftEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
        colliderpoints[1] = new Vector2(lDCorner.x, rUCorner.y);
        leftEdge.points = colliderpoints;

        EdgeCollider2D rightEdge = new GameObject("rightEdge").AddComponent<EdgeCollider2D>();

        colliderpoints = rightEdge.points;
        colliderpoints[0] = new Vector2(rUCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
        rightEdge.points = colliderpoints;
    }
}
