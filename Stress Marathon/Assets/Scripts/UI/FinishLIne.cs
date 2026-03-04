using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLIne : MonoBehaviour
{
    [SerializeField] private RaceBoard _raceBoard;
    [SerializeField] private LayerMask _entry;
    public static event Action<Runner> OnRunnerFinished;
    public static event Action<Dictionary<int, Runner>> OnPlayerFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (_entry.Contains(collision.gameObject))
        {
            Runner runner = collision.GetComponent<Runner>();
            OnRunnerFinished?.Invoke(runner);
            
            if (collision.CompareTag("Player"))
            {
                RaceFinish();
            }
        }
    }
    
    private void RaceFinish()
    {
        Dictionary<int, Runner> history = _raceBoard.RankHistory;
        
        GameManager.Instance.IsRacing = false;
        GameManager.Instance.IsPrized = true;
        
        OnPlayerFinished?.Invoke(history);
    }
}
