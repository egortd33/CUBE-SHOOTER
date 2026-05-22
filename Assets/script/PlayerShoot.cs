using UnityEngine;
using DG.Tweening;
using TMPro;
using System; // или using TextMeshPro; если используется 3D-текст

public class PlayerShoot : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 50f;

    [SerializeField] private Transform ViselModels;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float ballSpeed = 20f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int maxShots = 30;              // максимальное количество выстрелов

    [Header("Recoil Effect")]
    [SerializeField] private float recoilDistans = 0.3f;       // сила отдачи (на сколько отъезжает назад)
    [SerializeField] private float recoilDuration = 0.15f;   // длительность анимации отдачи
    [SerializeField] private int recoilVibret = 5;   // длительность анимации отдачи
    [SerializeField] private float recoilAlastic = 5;   // длительность анимации отдачи
    [SerializeField] private float recoilEndel = 5;   // длительность анимации отдачи


    [Header("Death Shrink")]
    [SerializeField] private float shrinkDuration = 0.5f;    // время уменьшения до 0

    [Header("UI")]
    [SerializeField] private TMP_Text ammoText;              // ссылка на TextMeshPro (UI или 3D)

    [Header("Current Color")]
    [SerializeField] private ColorType currentColor = ColorType.Red;

    private float nextFireTime;
    private bool targetInSight;
    public bool IsActive = false;

    private int currentShots;
    private bool isDead = false;                             // флаг, что персонаж уже "умер" (уменьшается)

    private void Start()
    {
        currentShots = maxShots;
        UpdateAmmoText();
    }

    private void Update()
    {
        if (!IsActive || isDead) return;

        targetInSight = PerformRaycastAndCheck();

        // Стреляем только если есть цель, прошло время кд и остались патроны
        if (targetInSight && Time.time >= nextFireTime && currentShots > 0)
        {
            ShootBall();
            nextFireTime = Time.time + fireRate;
        }
    }

    private bool PerformRaycastAndCheck()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.white);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            BlockColor block = hit.collider.GetComponent<BlockColor>();
            if (block != null && block.colorType == currentColor)
            {
                return true;
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

        // Создаём шар
        GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.assignedColor = currentColor;
            ballScript.speed = ballSpeed;
        }
        else
        {
            Debug.LogError("На префабе шарика нет компонента Ball!");
        }

        // Отключаем физику у Rigidbody, если есть
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        PlayRecloil();
        // --- Учёт выстрелов ---
        currentShots--;
        UpdateAmmoText();

        // Если патроны кончились – запускаем уменьшение
        if (currentShots <= 0)
        {
            StartShrinkAndDeactivate();
        }
    }

    private void PlayRecloil()
    {
        ViselModels.DOKill(true);

        ViselModels.DOPunchPosition(
            new Vector3(0, 0, -recoilDistans),
            recoilDuration,
            recoilVibret,
            recoilAlastic);

    }

    private void StartShrinkAndDeactivate()
    {
        if (isDead) return;
        isDead = true;

        // Останавливаем все твины на трансформе, чтобы не мешали
        ViselModels.DOKill(false);
        // Плавно уменьшаем scale до нуля
        ViselModels.DOScale(Vector3.zero, shrinkDuration)
                 .SetEase(Ease.InBack)   // можно выбрать любую кривую, например Ease.InBack для "схлопывания"
                 .OnComplete(() => gameObject.SetActive(false));
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentShots}";
        }
    }
}