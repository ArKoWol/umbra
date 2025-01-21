using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Игрок, за которым будет следовать камера
    public float smoothSpeed = 0.125f;  // Скорость сглаживания движения камеры
    public Vector3 offset;  // Смещение камеры относительно игрока

    void LateUpdate()
    {
        // Рассчитываем желаемую позицию камеры, с учетом смещения
        Vector3 desiredPosition = player.position + offset;

        // Плавно перемещаем камеру к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Обновляем позицию камеры
        transform.position = smoothedPosition;
    }
}