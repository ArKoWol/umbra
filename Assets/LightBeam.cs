using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    public Transform startPoint; // Начальная точка луча
    public Transform targetPoint; // Точка, куда направлен луч
    public LayerMask reflectionLayer; // Слой для объектов, которые могут отражать луч
    public int maxReflections = 5; // Максимальное количество отражений
    public float maxDistance = 50f; // Максимальная длина луча

    private LineRenderer lineRenderer; // Ссылка на LineRenderer

    private void Start()
    {
        // Получаем компонент LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLightBeam();
    }

    private void DrawLightBeam()
    {
        // Список точек, через которые проходит луч
        List<Vector3> points = new List<Vector3>();

        // Добавляем начальную точку
        Vector2 currentPosition = startPoint.position;
        points.Add(currentPosition);

        // Начальное направление луча от стартовой точки к целевой точке
        Vector2 direction = (targetPoint.position - startPoint.position).normalized;

        // Выполняем трассировку луча с отражениями
        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, maxDistance, reflectionLayer);

            if (hit.collider != null) // Если луч столкнулся с объектом
            {
                points.Add(hit.point); // Добавляем точку столкновения
                direction = Vector2.Reflect(direction, hit.normal); // Рассчитываем новое направление
                currentPosition = hit.point; // Перемещаем текущую позицию к точке столкновения
            }
            else
            {
                // Если луч не сталкивается с объектом, добавляем конечную точку
                points.Add(currentPosition + direction * maxDistance);
                break;
            }
        }

        // Обновляем LineRenderer
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
