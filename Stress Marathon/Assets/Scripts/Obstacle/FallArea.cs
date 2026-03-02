using System;
using Unity.VisualScripting;
using UnityEngine;

public class FallArea : MonoBehaviour
{
    [SerializeField] private LayerMask _runner;
    
    public Action<Collider2D> OnFallDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_runner.Contains(collision.gameObject)) OnFallDetected?.Invoke(collision);
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;
        
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(box.offset, box.size);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.offset, box.size);
    }
}
