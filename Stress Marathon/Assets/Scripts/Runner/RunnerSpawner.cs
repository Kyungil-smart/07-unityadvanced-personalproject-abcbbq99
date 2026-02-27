using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RunnerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _runnerObj;
    [SerializeField] private int _runnerNum = 7;
    
    private void Start()
    {
        for (int i = 0; i < _runnerNum; i++)
        {
            Instantiate(_runnerObj, transform.position, transform.rotation);
        }
    }
}
