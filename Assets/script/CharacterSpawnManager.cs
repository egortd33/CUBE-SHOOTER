using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterSpawnManager : MonoBehaviour
{
    public static CharacterSpawnManager Instance { get; private set; }

    [Header("Префабы персонажей (с уже настроенным цветом в PlayerShoot)")]
    [SerializeField] private List<GameObject> characterPrefabs;

    [Header("Точка спавна (опционально)")]
    [SerializeField] private Transform spawnPoint;

    // Живая коллекция заспавненных персонажей
    private List<PlayerShoot> spawnedCharacters = new List<PlayerShoot>();
    public IReadOnlyList<PlayerShoot> SpawnedCharacters => spawnedCharacters;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Спавнит персонажа нужного цвета. Префаб ищется по цвету в его компоненте PlayerShoot.
    /// </summary>
    public GameObject SpawnCharacter(ColorType color, Vector3 defaultPosition)
    {
        // Ищем префаб, у которого PlayerShoot.currentColor совпадает
        GameObject targetPrefab = null;
        foreach (var prefab in characterPrefabs)
        {
            if (prefab == null) continue;

            PlayerShoot ps = prefab.GetComponent<PlayerShoot>();
            if (ps != null && ps.currentColor == color)
            {
                targetPrefab = prefab;
                break;
            }
        }

        if (targetPrefab == null)
        {
            Debug.LogError($"Префаб для цвета {color} не найден!");
            return null;
        }

        Vector3 pos = spawnPoint != null ? spawnPoint.position : defaultPosition;
        Quaternion rot = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;
        GameObject newChar = Instantiate(targetPrefab, pos, rot);

        PlayerShoot newPs = newChar.GetComponent<PlayerShoot>();
        if (newPs != null)
        {
            spawnedCharacters.Add(newPs);
        }
        else
        {
            Debug.LogWarning("Спавненный персонаж не содержит PlayerShoot!");
        }

        return newChar;
    }

    /// <summary>
    /// Удаляет персонаж из живой коллекции.
    /// </summary>
    public void RemoveCharacter(PlayerShoot character)
    {
        if (character != null && spawnedCharacters.Contains(character))
            spawnedCharacters.Remove(character);
    }

    /// <summary>
    /// Возвращает первый живой персонаж с указанным цветом (или null).
    /// </summary>
    public PlayerShoot GetCharacterByColor(ColorType color)
    {
        return spawnedCharacters.FirstOrDefault(ps => ps.currentColor == color);
    }
}