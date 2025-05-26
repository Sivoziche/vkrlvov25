using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public Color gizmoColor = Color.red;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Debug.Log("Финиш!");
    }
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}