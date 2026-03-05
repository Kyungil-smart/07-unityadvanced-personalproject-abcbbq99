using System;
using UnityEngine;

public class RespawnSupporter : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;
    [SerializeField] FallArea _fallArea;

    private void OnEnable()
    {
        if(_fallArea == null) return;
        _fallArea.OnFallDetected += HandleRespawn;
    }

    private void OnDisable()
    {
        _fallArea.OnFallDetected -= HandleRespawn;
    }

    private void HandleRespawn(Collider2D other)
    {
        Runner runner = other.GetComponent<Runner>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        
        runner.transform.position = _respawnPoint.position;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        runner.IsHit = true;
    }
}
