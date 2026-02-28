using System;
using System.Collections;
using UnityEngine;

public class SawSpawner : MonoBehaviour
{
    [SerializeField] private float _sawLifeTime = 10f;
    [SerializeField] private RollingSaw _sawPrefab;
    [SerializeField] private float _spwanDelayTime = 3f;
    
    private ObjectPool<RollingSaw> _rollingSawPool;
    private float _delaytime;

    private void Awake()
    {
        _rollingSawPool = new ObjectPool<RollingSaw>(_sawPrefab, 10, true);
        _delaytime = _spwanDelayTime;
    }

    private void Update()
    {
        DelayCounter();
    }

    private void DelayCounter()
    {
        _delaytime -= Time.deltaTime;

        if (_delaytime <= 0f)
        {
            SetSpawn();
            _delaytime = _spwanDelayTime;
        }
    }

    private void SetSpawn()
    {
        if(!GameManager.IsRacing) return;
        RollingSaw saw = _rollingSawPool.Spwan();
        saw.OnLifeTimeEnd += ReturnToPool;
        saw.SetUp(transform.position, transform.rotation, _sawLifeTime);
    }

    private void ReturnToPool(RollingSaw saw)
    {
        saw.OnLifeTimeEnd -= ReturnToPool;
        _rollingSawPool.Despawn(saw);
    }
}
