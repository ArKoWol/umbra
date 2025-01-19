using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectivePlatform : MonoBehaviour
{
    public float rotationSpeed = 90f; // Скорость вращения платформы
    private bool isNearPlayer = false; // Флаг, показывающий, что персонаж рядом

    // Вращение платформы влево или вправо с помощью клавиш
    private void Update()
    {
        if (isNearPlayer && Input.GetKey("k"))
        {
            if (Input.GetKey("j"))
            {
                RotatePlatform(-rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey("l"))
            {
                RotatePlatform(rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void RotatePlatform(float angle)
    {
        transform.Rotate(0f, 0f, angle); // Вращаем платформу вокруг оси Z
    }

    // Когда персонаж входит в зону действия платформы
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    // Когда персонаж выходит из зоны действия платформы
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}
