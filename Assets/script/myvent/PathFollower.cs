using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [Header("Настройки движения")]
    [Tooltip("Массив точек пути (Transform). Позиция и поворот каждой точки задают целевую позу.")]
    public Transform[] waypoints;            // массив точек
    public float moveSpeed = 3f;             // скорость перемещения
    public float rotationSpeed = 5f;         // скорость поворота (чем больше, тем резче)
    public bool loop = true;                 // зациклить ли путь

    [Header("Текущее состояние")]
    [SerializeField] private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform targetWP = waypoints[currentWaypointIndex];

        // Плавное перемещение к целевой точке
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWP.position,
            moveSpeed * Time.deltaTime
        );

        // Плавный поворот к целевому повороту точки
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetWP.rotation,
            rotationSpeed * Time.deltaTime
        );

        // Проверка, достигли ли мы точки (с учётом расстояния)
        if (Vector3.Distance(transform.position, targetWP.position) < 0.01f)
        {
            // Переход к следующей точке
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                if (loop)
                    currentWaypointIndex = 0;
                else
                    enabled = false; // остановить скрипт, если не зациклено
            }
        }
    }

    // Отрисовка Gizmos в редакторе – визуализация поворота точек пути
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Gizmos.color = Color.green; // цвет для точек

        foreach (Transform wp in waypoints)
        {
            if (wp == null) continue;

            // Сфера в позиции точки
            Gizmos.DrawSphere(wp.position, 0.2f);

            // Ось вперёд (синяя) – показывает поворот точки
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(wp.position, wp.forward * 0.5f);

            // Ось вверх (зелёная) – для наглядности
            Gizmos.color = Color.green;
            Gizmos.DrawRay(wp.position, wp.up * 0.3f);

            // Ось вправо (красная) – для наглядности
            Gizmos.color = Color.red;
            Gizmos.DrawRay(wp.position, wp.right * 0.3f);

            // Вернуть цвет для следующей сферы
            Gizmos.color = Color.green;
        }

        // Соединительные линии между точками
        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;
            int next = (i + 1) % waypoints.Length;
            if (waypoints[next] == null) continue;
            Gizmos.DrawLine(waypoints[i].position, waypoints[next].position);
        }
    }
}