using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShift : GravityShift
{
    public float shiftDuration = 2.0f; // Time to complete inversion
    private bool isShifting = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    void Update()
    {
        if (IsToggleGravity())
        {
            ToggleGravity();
        }

        if (IsToggleGravity()&& !isShifting)
        {
            StartCoroutine(SmoothShift());
        }
    }

    private IEnumerator SmoothShift()
    {
        isShifting = true;
        float elapsedTime = 0f;

        startPosition = transform.position;
        float objectHeight = 12;
        
        if (!mIsGravityUp) 
            objectHeight *= -1;

        targetPosition = new Vector3(startPosition.x, startPosition.y + objectHeight, startPosition.z);

        while (elapsedTime < shiftDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / shiftDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isShifting = false;
    }
}
