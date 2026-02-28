using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPoolable
{
    private T _prefab;
    private Queue<T> _pool;
    public bool IsExpandable{get; set;}
    public bool IsEmpty => _pool.Count == 0;

    public ObjectPool(T prefab, int capacity = 10, bool isExpandable = false)
        => Init(prefab, capacity, isExpandable);
    
    private void Init(T prefab, int capacity, bool isExpandable)
    {
        _prefab = prefab;
        IsExpandable = isExpandable;
        _pool = new Queue<T>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            T pooledObject = CraetePooledObject();
            Despawn(pooledObject);
        }
    }
    
    public T Spwan()
    {
        T pooledObject;

        if (IsExpandable && IsEmpty) pooledObject = CraetePooledObject();
        else pooledObject = _pool.Dequeue();
        
        pooledObject.OnSpawn();
        
        return pooledObject;
    }

    public void Despawn(T pooledObject)
    {
        pooledObject.OnDespawn();
        _pool.Enqueue(pooledObject);
    }
    
    public void DisposePooledObjects(int count)
    {
        count = Mathf.Min(count, _pool.Count);

        for (int i = 0; i < count; i++)
        {
            T pooledObject = _pool.Dequeue();
            pooledObject.OnDispose();
        }
    }
    
    private T CraetePooledObject()
    {
        T pooledObject = MonoBehaviour.Instantiate(_prefab);
        pooledObject.OnCreate();
        return pooledObject;
    }
    
    
}
