using UnityEngine;

public class TeleportPlayerToStart : MonoBehaviour
{
    // Укажите начальную позицию вручную, если нужно
    [SerializeField] private Transform startPoint;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            // Телепортируем игрока в указанную точку
            other.transform.position = startPoint.position;
        }
    }
}
