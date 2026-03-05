using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RollingSaw : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _entry;
    [SerializeField] private float _knockbackForce = 15f;
    
    public event Action<RollingSaw> OnLifeTimeEnd;
    
    private Rigidbody2D _rb;
    private float _timeCount;
    
    private Runner _runner;

    private void Update()
    {
        HandleTimeOut();
    }

    public void OnCreate()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public void OnDispose()
    {
        Destroy(gameObject);
    }
    
    public void SetUp(Vector2 position, Quaternion rotation, float lifetime)
    {
        transform.SetPositionAndRotation(position, rotation);
        _rb.linearVelocity = Vector2.zero;
        _rb.AddTorque(Random.Range(-50f, 50f), ForceMode2D.Impulse);
        _rb.AddForce(new Vector2(Random.Range(-2f, 0), 0), ForceMode2D.Impulse);
        _timeCount = lifetime;
    }
    
    private void HandleTimeOut()
    {
        _timeCount -= Time.deltaTime;
        if (_timeCount <= 0)
        {
            OnLifeTimeEnd?.Invoke(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_entry.Contains(other.collider.gameObject))
        {
            _runner = other.gameObject.GetComponent<Runner>();
            switch (_runner.IsHit)
            {
                case true:
                    return;
                case false:
                    OnHit(other, _runner);
                    _runner.IsHit = true;
                    break;
            }
        }
    }
    
    private void OnHit(Collision2D other, Runner runner)
    {
        Vector2 dir = (other.transform.position - transform.position).normalized;
        dir.y = 0.5f;
        
        _runner.Rb.linearVelocity = Vector2.zero;
        _runner.Rb.AddForce(dir * _knockbackForce, ForceMode2D.Impulse);
    }
    
    
}
