using UnityEngine;
using DG.Tweening;

public class ScaleFromZero : MonoBehaviour
{
    [Header("Настройки анимации")]
    [SerializeField] private float _duration = 0.5f;   // Длительность анимации
    [SerializeField] private Ease _ease = Ease.OutBack; // Тип плавности
    [SerializeField] private bool _playOnStart = true;  // Запускать ли при старте

    private Vector3 _originalScale; // Запомненный исходный масштаб

    private void Awake()
    {
        // Запоминаем текущий масштаб персонажа
        _originalScale = transform.localScale;

        if (_playOnStart)
        {
            PlayScaleAnimation();
        }
    }

    /// <summary>
    /// Устанавливает масштаб в ноль и плавно возвращает к запомненному масштабу.
    /// </summary>
    public void PlayScaleAnimation()
    {
        // Сначала делаем масштаб нулевым
        transform.localScale = Vector3.zero;

        // Плавно скейлимся до исходного масштаба
        transform.DOScale(_originalScale, _duration)
            .SetEase(_ease)
            .SetUpdate(true); // Игнорирует Time.timeScale, чтобы анимация работала даже при паузе
    }
    
}