using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineController : MonoBehaviour
{
    public List<Transform> waypoints; // Точки маршрута
    private List<Vector3> _splinePoints;
    private float[] _cumulativeLengths;
    [HideInInspector] public List<Vector3> SplinePoints; // Делаем публичным

    void Start()
    {
        SplinePoints = new List<Vector3>(); // Изменяем _splinePoints на SplinePoints
        for (float t = 0; t <= 1; t += 0.01f)
        {
            SplinePoints.Add(CalculateCatmullRom(t));
        }
        PrecomputeLengths();
    }

    Vector3 CalculateCatmullRom(float t)
    {
        // Реализация Catmull-Rom сплайна через 4 точки
        int numPoints = waypoints.Count;
        int segment = Mathf.FloorToInt(t * (numPoints - 3));
        t = t * (numPoints - 3) - segment;

        Vector3 p0 = waypoints[segment].position;
        Vector3 p1 = waypoints[segment + 1].position;
        Vector3 p2 = waypoints[segment + 2].position;
        Vector3 p3 = waypoints[segment + 3].position;

        return 0.5f * ((-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t
                     + (2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t
                     + (-p0 + p2) * t
                     + 2f * p1);
    }

    void PrecomputeLengths()
    {
        // Для поиска ближайшей точки на сплайне
        _cumulativeLengths = new float[_splinePoints.Count];
        float totalLength = 0;
        for (int i = 1; i < _splinePoints.Count; i++)
        {
            totalLength += Vector3.Distance(_splinePoints[i], _splinePoints[i - 1]);
            _cumulativeLengths[i] = totalLength;
        }
    }

    public Vector3 GetPositionAlongSpline(float progress)
    {
        // Прогресс от 0 до 1
        float targetLength = progress * _cumulativeLengths[_cumulativeLengths.Length - 1];
        int index = System.Array.BinarySearch(_cumulativeLengths, targetLength);
        if (index < 0) index = ~index;
        return _splinePoints[index];
    }
}