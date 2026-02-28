using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class RunnerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _runnerObj;
    [SerializeField] private int _runnerNum = 7;
    
    private void Start()
    {
        for (int i = 0; i < _runnerNum; i++)
        {
            GameObject newNPC = Instantiate(_runnerObj, transform.position, transform.rotation);
            Runner runner = newNPC.GetComponent<Runner>();
            runner.RunnerName = "NPC" + Random.Range(100, 999);
        }
    }
}
