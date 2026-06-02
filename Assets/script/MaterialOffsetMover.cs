using UnityEngine;
using DG.Tweening;

public class MaterialOffsetMover : MonoBehaviour
{
    [Header("Основная текстура (_MainTex)")]
    [SerializeField] private float targetOffsetX = 0.5f;   // желаемое смещение по X
    [SerializeField] private float duration = 1f;         // длительность анимации
    [SerializeField] private Ease ease = Ease.Linear;     // кривая анимации
    [SerializeField] private int loops = -1;              // -1 = бесконечно, 0 = один раз, 1 = туда-обратно и т.д.
    [SerializeField] private LoopType loopType = LoopType.Restart;

    private Material mat;
    private Tween offsetTween;

    void Awake()
    {
        // Получаем материал (создаётся копия, чтобы не влиять на общий материал)
        var rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("Renderer не найден на объекте!");
            return;
        }
        mat = rend.material;
    }

    void Start()
    {
        StartOffsetAnimation();
    }

    /// <summary>
    /// Запускает анимацию смещения по X. Текущее значение Y сохраняется.
    /// </summary>
    public void StartOffsetAnimation()
    {
        // Останавливаем предыдущую анимацию, если была
        KillTween();

        Vector2 currentOffset = mat.mainTextureOffset;
        Vector2 endOffset = new Vector2(targetOffsetX, currentOffset.y);

        offsetTween = mat.DOOffset(endOffset, duration)
            .SetEase(ease)
            .SetLoops(loops, loopType)
            .SetId("MaterialOffsetX"); // идентификатор для безопасного убийства
    }

    /// <summary>
    /// Мгновенно останавливает анимацию и возвращает offset к исходному (если нужно).
    /// </summary>
    public void StopAnimation(bool resetToStart = false)
    {
        KillTween();
        if (resetToStart)
            mat.mainTextureOffset = new Vector2(0f, mat.mainTextureOffset.y);
    }

    private void KillTween()
    {
        DOTween.Kill("MaterialOffsetX");
    }

    void OnDestroy()
    {
        KillTween();
        // Если создавали копию материала через .material, можно её уничтожить
        if (mat != null)
            Destroy(mat);
    }

    // Пример вызова из другого скрипта или UI
    [ContextMenu("Toggle Animation")]
    public void ToggleAnimation()
    {
        if (DOTween.IsTweening("MaterialOffsetX"))
            StopAnimation(true);
        else
            StartOffsetAnimation();
    }
}