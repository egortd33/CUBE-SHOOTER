using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    [Header("Элементы экрана загрузки")]
    public Slider progressSlider;         // Слайдер прогресса
    public GameObject loadingScreen;      // Сам экран загрузки (Canvas/панель)

    /// <summary>
    /// Запускает асинхронную загрузку сцены по индексу.
    /// </summary>
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        // Показываем экран загрузки
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Начинаем асинхронную загрузку
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // Запрещаем автоматическую активацию сцены до полной готовности
        asyncLoad.allowSceneActivation = false;

        float displayProgress = 0f;

        // Ждём завершения загрузки
        while (!asyncLoad.isDone)
        {
            // asyncLoad.progress растёт от 0 до 0.9 (90% — загрузка завершена)
            // Приводим к диапазону 0..1
            displayProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Обновляем значение слайдера
            if (progressSlider != null)
                progressSlider.value = displayProgress;

            // Когда реальный прогресс достиг 0.9, загрузка готова к активации
            if (asyncLoad.progress >= 0.9f)
            {
                // Можно сразу выставить слайдер на 100%
                if (progressSlider != null)
                    progressSlider.value = 1f;

                // Активируем сцену
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // Сцена полностью загружена и активна — прячем экран загрузки
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }
}