using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class WorldSpavanButton : MonoBehaviour
{
    [SerializeField] private ColorType buttonColor;   // цвет, за который отвечает эта кнопка

    private PersonRowManager rowManager;

    private Transform ObjectScale;

    private float ScaleDuration = 0.5f;

    private void Awake()
    {
        rowManager = GetComponentInParent<PersonRowManager>();
        if (rowManager == null)
        {
            Debug.LogError($"PersonRowManager не найден в родительских объектах для {gameObject.name}", this);
        }
    }

    private void Start()
    {
        ObjectScale = transform;
    }

    public void DestroyAnimes()
    {
        ObjectScale.DOScale(Vector3.zero, ScaleDuration)
            .SetEase(Ease.InBack)
            .OnComplete(()=>
            {
                rowManager.Unlock_M();
                Destroy(ObjectScale.gameObject);
            }); 
    }

    private void OnMouseDown()
    {
        if (rowManager == null) return;

        if (rowManager.IsfiIsFirstInQueue(gameObject))
        {
            var manager = CharacterSpawnManager.Instance;

            // Пробуем найти живого персонажа с таким же цветом
            PlayerShoot existing = null;
            foreach (var ps in manager.SpawnedCharacters)
            {
                if (ps.currentColor == buttonColor)
                {
                    existing = ps;
                    break;
                }
            }

            if (existing != null)
            {
                Debug.Log($"Персонаж цвета {buttonColor} уже есть: {existing.name}");
                // Здесь можно выполнить свою логику
            }
            else
            {
                Debug.Log($"Персонаж цвета {buttonColor} не найден. Спавним нового.");
                manager.SpawnCharacter(buttonColor, transform.position);
            }

            // Удаляем кнопку из очереди
            rowManager.Perons.Remove(transform);
        }
        else
        {
            Debug.Log("Можно взаимодействовать только с первым объектом в очереди");
        }
        rowManager.Lock_M();

        DestroyAnimes();
    }
}