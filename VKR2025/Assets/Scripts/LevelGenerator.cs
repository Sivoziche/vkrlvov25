using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    public int width;  // ������ �����
    public int height; // ������ �����
    public GameObject[] obstacles; // ������� �����������
    public GameObject startPrefab; // ������ ������
    public GameObject finishPrefab; // ������ ������
    public Camera mainCamera; // ������ �� ������
    public float minDistance = 2f; // ����������� ���������� ����� ���������

    private int[,] map; // ��������� ������ ��� �������� �����
    private List<Vector3> spawnedPositions = new List<Vector3>(); // ������ ������� ��������� ��������

    void Start()
    {
        width = 20;
        height = 10;
        map = new int[width, height];
        GenerateMap();
        SpawnLevel();
        AdjustCamera();
    }

    void GenerateMap()
    {
        // ������� �����
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = 0; // 0 - ������ ������������
            }
        }

        // ���������� ������
        int startY = Random.Range(0, height);
        map[0, startY] = 1; // 1 - �����

        // ���������� ������
        int finishY = Random.Range(0, height);
        map[width - 1, finishY] = 2; // 2 - �����

        // �������� ���� ������ ������ � ������
        int bufferZone = 2; // ����������, �� ������� ����������� �� ����� ��������������

        // ���������� �����������
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool isNearStart = x < bufferZone;
                bool isNearFinish = x > width - 1 - bufferZone;

                if ((map[x, y] == 0) && !isNearFinish && !isNearStart) /* && Random.Range(0, 100) < 10*/ // 10% chance to spawn an obstacle
                {
                    map[x, y] = 4;//Random.Range(3, 3 + obstacles.Length); // Random obstacle type
                }
            }
        }
    }

    void SpawnLevel()
    {
        float centerX = width / 2f; // ����� �� X
        float centerY = height / 2f; // ����� �� Y

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // ������� ������� ������� ���, ����� ����� ����� ��� � (0, 0)
                Vector3 position = new Vector3(x - centerX, y - centerY, 0); // Random.Range(-0.5f, 0.5f)

                if (map[x, y] == 1)
                {
                    Instantiate(startPrefab, position, Quaternion.identity);
                    spawnedPositions.Add(position);
                }
                else if (map[x, y] == 2)
                {
                    Instantiate(finishPrefab, position, Quaternion.identity);
                    spawnedPositions.Add(position);
                }
                else if (map[x, y] >= 3)
                {
                    if (IsPositionValid(position)/* && IsPositionInsideCamera(position)*/)
                    {
                        int obstacleIndex = map[x, y] - 3;
                        Instantiate(obstacles[obstacleIndex], position, Quaternion.identity);
                        spawnedPositions.Add(position);
                    }
                   /* else
                    {
                        int obstacleIndex = map[x, y] - 3;
                        int attempts = 10; // ���������� �������
                        for (int i = 0; i < attempts; i++)
                        {
                            Vector3 newPosition = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                            if (IsPositionValid(newPosition))
                            {
                                Instantiate(obstacles[obstacleIndex], newPosition, Quaternion.identity);
                                spawnedPositions.Add(newPosition);
                                break;
                            }
                        }
                    }*/
                }
            }
        }
    }

    /*bool IsPositionInsideCamera(Vector3 position)
    {
        // �������� ������� ������
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // ���������, ��������� �� ������� � �������� ������
        return position.x >= -cameraWidth / 2f && position.x <= cameraWidth / 2f &&
               position.y >= -cameraHeight / 2f && position.y <= cameraHeight / 2f;
    }*/

    bool IsPositionValid(Vector3 position)
    {
        foreach (var spawnedPosition in spawnedPositions)
        {
            // ��������� ���������� ����� ������� �������� � ��� ���������� ���������
            if (Vector3.Distance(position, spawnedPosition) < minDistance)
            {
                return false; // ������� ������� ������ � ������� �������
            }
        }
        return true; // ������� ���������
    }

    void AdjustCamera()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float mapWidth = width;
        float mapHeight = height;

        float maxObstacleSize = GetMaxObstacleSize();
        float cameraSize = Mathf.Max((mapHeight / 2f) + maxObstacleSize / 2f, mapWidth / (2f * aspectRatio));
        // ������� ������ ����� �� �������� ������ �����
        /*mainCamera.transform.position = new Vector3(0, mapHeight / 10f, -10); // -10 ��� ��������������� ������*/
        mainCamera.orthographicSize = cameraSize;
    }

    float GetMaxObstacleSize()
    {
        float maxSize = 0f;
        foreach (var obstacle in obstacles)
        {
            // ������������, ��� ����������� ����� ��������� SpriteRenderer ��� MeshRenderer
            Renderer renderer = obstacle.GetComponent<Renderer>();
            if (renderer != null)
            {
                float size = Mathf.Max(renderer.bounds.size.x, renderer.bounds.size.y);
                if (size > maxSize)
                {
                    maxSize = size;
                }
            }
        }
        return maxSize;
    }
}
