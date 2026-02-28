using UnityEngine;

public interface IPoolable
{
    public void OnCreate();
    public void OnSpawn();
    public void OnDespawn();
    public void OnDispose();
}
