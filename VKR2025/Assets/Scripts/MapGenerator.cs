using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapGenerator.cs
public class MapGenerator : MonoBehaviour
{
    [Header("Settings")]
    public int minObjects = 3;
    public int maxObjects = 7;
    public float spawnRadius = 50f;

    public GameObject gravityObjectPrefab;
    public GameObject asteroidPrefab;

    void GenerateMap()
    {
        int numObjects = Random.Range(minObjects, maxObjects + 1);

        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = Random.insideUnitSphere * spawnRadius;
            Quaternion rot = Random.rotation;

            if (Random.value > 0.5f)
            {
                Instantiate(gravityObjectPrefab, pos, rot);
            }
            else
            {
                Instantiate(asteroidPrefab, pos, rot);
            }
        }
    }

    void Start() => GenerateMap();
}
