using System;
using UnityEngine;

public class CamaraPointGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(18,10));
        
        Gizmos.color = Color.lightGoldenRod;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
