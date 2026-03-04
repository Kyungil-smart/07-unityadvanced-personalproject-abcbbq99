using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] int _startingWaypoint;
    [SerializeField] private List<Vector2> _path = new List<Vector2>();
    
    private int _currentWaypoint;

    private void Awake()
    {
        if(_startingWaypoint < _path.Count && _startingWaypoint >= 0) 
            _currentWaypoint = _startingWaypoint;
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;
        int endPoint = _path.Count - 1;
        
        for (int i = 0; i < endPoint; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_path[i], _path[i + 1]);
            
            Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
            Gizmos.DrawCube(_path[i], box.size);
        
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_path[i], box.size);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_path[endPoint], _path[0]);
        
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(_path[endPoint], box.size);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_path[endPoint], box.size);
    }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        if(!GameManager.Instance.IsRacing) return;
        
        if (_path == null || _path.Count <= 0 || _currentWaypoint > _path.Count - 1) return;
        
        if (Vector2.Distance(_path[_currentWaypoint], transform.position) <= 0.05f)
        {
            _currentWaypoint++;
            
            if (_currentWaypoint >= _path.Count)  _currentWaypoint = 0;
        }
        
        transform.position = Vector2.MoveTowards(
            transform.position, 
            _path[_currentWaypoint], 
            Time.deltaTime * _moveSpeed);
    }
    
}
