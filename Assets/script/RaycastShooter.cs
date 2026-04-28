using UnityEngine;

public class RaycastShooter : MonoBehaviour
{
    [Header("Настройки рейкаста")]
    [SerializeField] private float maxDistance = 50f;      // Дальность луча

    [Header("Визуализация")]
    [SerializeField] private bool drawDebugRay = true;     // Рисовать луч в Scene view
    [SerializeField] private Color rayColor = Color.red;   // Цвет луча

    private void Update()
    {
        // Стрельба по нажатию левой кнопки мыши (можно заменить на любую клавишу)
        if (Input.GetButtonDown("Fire1"))
        {
            ShootRaycast();
        }

        // Постоянная отрисовка луча в редакторе (для наглядности)
        if (drawDebugRay)
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, rayColor);
        }
    }

    private void ShootRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // Сам рейкаст
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Попадание зафиксировано
            Debug.Log($"Попадание в: {hit.collider.name} | Точка: {hit.point}");

            // Здесь можно добавить нанесение урона, эффекты и т.д.
            // Например, если у цели есть компонент здоровья:
            // Health health = hit.collider.GetComponent<Health>();
            // if (health != null) health.TakeDamage(damage);
        }
        else
        {
            // Промах
            Debug.Log("Промах");
        }
    }
}