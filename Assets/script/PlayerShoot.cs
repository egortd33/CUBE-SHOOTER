using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    [SerializeField] private int maxShots = 30;            // Максимальное количество выстрелов
    [SerializeField] private TextMeshProUGUI shotsText;    // Ссылка на TextMeshPro для отображения счётчика

    [Header("Эффект отдачи (отскок назад)")]
    [SerializeField] private float recoilDistance = 0.3f;  // Насколько сильно персонаж отлетает назад
    [SerializeField] private float recoilDuration = 0.1f;  // Длительность отдачи

    [Header("Уменьшение после всех выстрелов")]
    [SerializeField] private float shrinkDuration = 1f;    // Время уменьшения до 0
    [SerializeField] private Ease shrinkEase = Ease.InBack;// Тип анимации уменьшения

    private int currentShots;          // Текущее количество сделанных выстрелов
    private bool canShoot = true;      // Можно ли стрелять

    private void Start()
    {
        currentShots = 0;
        UpdateUI();
    }

    private void Update()
    {
        // Для примера: выстрел по нажатию левой кнопки мыши или пробела.
        // Вы можете заменить это на вызов метода Shoot() из другого скрипта.
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Метод выстрела. Увеличивает счётчик, проигрывает отдачу,
    /// обновляет UI и запускает уменьшение при достижении лимита.
    /// </summary>
    public void Shoot()
    {
        if (!canShoot)
            return;

        // Увеличиваем счётчик выстрелов
        currentShots++;
        UpdateUI();

        // --- Эффект отдачи (отскок назад) ---
        // Используем DOPunchPosition: персонаж резко уходит назад и возвращается.
        // punchDirection = -transform.forward даёт толчок в направлении, противоположном взгляду.
        Vector3 punch = -transform.forward * recoilDistance;
        transform.DOPunchPosition(punch, recoilDuration, vibrato: 0, elasticity: 0f)
                 .SetId("Recoil"); // ID, чтобы можно было при необходимости убить твин

        // Если хотите, чтобы персонаж просто отъезжал назад и не возвращался,
        // замените строчку выше на:
        // transform.DOMove(transform.position - transform.forward * recoilDistance, recoilDuration)
        //          .SetRelative().SetEase(Ease.OutQuad);

        // Проверяем, не достигнут ли лимит выстрелов
        if (currentShots >= maxShots)
        {
            canShoot = false;
            StartShrink();
        }
    }

    /// <summary>
    /// Запускает плавное уменьшение персонажа до нулевого размера.
    /// </summary>
    private void StartShrink()
    {
        // Убиваем твин отдачи, если он ещё активен, чтобы не мешал
        DOTween.Kill("Recoil");

        // Анимация масштаба до (0,0,0)
        transform.DOScale(Vector3.zero, shrinkDuration)
                 .SetEase(shrinkEase)
                 .OnComplete(() =>
                 {
                     // Действие после полного исчезновения (например, уничтожить объект)
                     // Destroy(gameObject);
                     Debug.Log("Персонаж полностью уменьшился");
                 });
    }

    /// <summary>
    /// Обновляет текст на экране.
    /// </summary>
    private void UpdateUI()
    {
        if (shotsText != null)
        {
            shotsText.text = $"Выстрелы: {currentShots}/{maxShots}";
        }
    }
}