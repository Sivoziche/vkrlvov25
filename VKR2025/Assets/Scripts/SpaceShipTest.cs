using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpaceshipTest.cs
public class SpaceshipTest : MonoBehaviour
{
    [Header("Movement")]
    public float thrustForce = 5f;
    public float rotationSpeed = 30f;

    [Header("Debug")]
    public bool showSplinePath = true;

    private Rigidbody _rb;
    private SplineController _splineController;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _splineController = GetComponent<SplineController>();
    }

    void Update()
    {
        // Ручное управление для тестов
        float thrust = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        _rb.AddForce(transform.forward * thrust * thrustForce);
        _rb.AddTorque(transform.up * rotation * rotationSpeed);
    }

    void OnDrawGizmos()
    {
        if (!showSplinePath || _splineController == null) return;
        foreach (var point in _splineController.SplinePoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
