using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public SplineController splineController;
    public float baseSpeed = 10f;
    public float gravityStrengthMultiplier = 1f;
    private Rigidbody _rb;
    private float _currentProgress;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentProgress = 0f;
    }

    void FixedUpdate()
    {
        // ������� �������� �� �������
        _currentProgress += baseSpeed * Time.fixedDeltaTime;
        Vector3 targetPos = splineController.GetPositionAlongSpline(_currentProgress);

        // ���������� �� �������� � ����� "GravityObject"
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GravityObject"))
        {
            Vector3 direction = obj.transform.position - transform.position;
            float distance = direction.magnitude;
            if (distance < 1f) distance = 1f; // ����� �������� ������� �� ����

            float forceMagnitude = gravityStrengthMultiplier * obj.GetComponent<Rigidbody>().mass / Mathf.Pow(distance, 2);
            _rb.AddForce(direction.normalized * forceMagnitude, ForceMode.Acceleration);
        }

        // ��������� ������� � �������
        Vector3 correctionForce = (targetPos - transform.position) * 5f;
        _rb.AddForce(correctionForce, ForceMode.Acceleration);
    }
}
