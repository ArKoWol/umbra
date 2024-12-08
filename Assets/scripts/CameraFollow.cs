using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Персонаж, за которым будет следовать камера
    public float smoothSpeed = 0.1f;
    public Vector3 offset;

    private void Start()
    {
        if (target == null)
        {
            GameObject characterObj = GameObject.FindGameObjectWithTag("Player");
            if (characterObj != null)
            {
                target = characterObj.transform;
                Debug.Log("Target found!");
            }
            else
            {
                Debug.LogWarning("No target with tag 'Player' found.");
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is missing!");
            return;
        }

        try
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"CameraFollow error: {ex.Message}");
        }
    }
}

