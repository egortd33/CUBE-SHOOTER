using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 50f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float ballSpeed = 20f;    // для префаба Ball теперь не нужно, но можно оставить для передачи
    [SerializeField] private float fireRate = 0.5f;

    [Header("Current Color")]
    [SerializeField] private ColorType currentColor = ColorType.Red;

    private float nextFireTime;
    private bool targetInSight;   // флаг, есть ли кубик в прицеле
    public bool IsActive = false; // флаг, есть ли кубик в прицеле


    private void Update()
    {
        if (!IsActive) return;
        // Рейкаст и определение цели
        targetInSight = PerformRaycastAndCheck();

        // Автоматический выстрел, только если есть цель и прошло время
        if (targetInSight && Time.time >= nextFireTime)
        {
            ShootBall();
            nextFireTime = Time.time + fireRate;
        }
        // Если цель пропала, таймер не сбрасываем резко – просто ждём нового попадания
    }

    // Возвращает true, если рейкаст попал в кубик с BlockColor
    private bool PerformRaycastAndCheck()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.white);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            BlockColor block = hit.collider.GetComponent<BlockColor>();
            if (block != null)
            {
                Debug.Log("В прицеле кубик цвета: " + block.colorType);
                if(block.colorType == currentColor)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void ShootBall()
    {
        if (ballPrefab == null || firePoint == null)
        {
            Debug.LogWarning("ballPrefab или firePoint не назначены!");
            return;
        }

        GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.assignedColor = currentColor;
            ballScript.speed = ballSpeed;   // передаём скорость (можно и напрямую в префабе задавать)
        }
        else
        {
            Debug.LogError("На префабе шарика нет компонента Ball!");
        }

        // Rigidbody больше не обязателен, но если есть – не мешает
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // чтобы физика не сдвигала шарик
        }
    }
}