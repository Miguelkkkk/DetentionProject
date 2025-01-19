using UnityEngine;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private int spawnCount = 4;
    [SerializeField] private float minDistance = 1.0f; 

    private List<Vector2> spawnedPositions = new List<Vector2>(); 

    private void OnEnable()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (prefabsToSpawn == null || prefabsToSpawn.Count == 0 || spawnArea == null)
        {
            Debug.LogWarning("Certifique-se de que os prefabs e a área de spawn foram configurados.");
            return;
        }

        // Obtém os limites da área de spawn
        Bounds bounds = spawnArea.GetComponent<Renderer>().bounds;

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];

            Vector2 randomPosition;
            int attempts = 0;

            do
            {
                randomPosition = new Vector2(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y)
                );

                attempts++;
                if (attempts > 100)
                {
                    Debug.LogWarning("Não foi possível encontrar uma posição válida após 100 tentativas.");
                    return;
                }

            } while (!IsPositionValid(randomPosition));

            spawnedPositions.Add(randomPosition);

            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }

    private bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 spawnedPosition in spawnedPositions)
        {
            if (Vector2.Distance(position, spawnedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
