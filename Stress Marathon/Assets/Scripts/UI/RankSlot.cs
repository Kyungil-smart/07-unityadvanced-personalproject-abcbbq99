using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class RankSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentRank;
    [SerializeField] TextMeshProUGUI _runnerName;
    [SerializeField] GameObject _playerHightlight;
    
    public Runner Owner{get; private set;}
    public int CurrentRank;

    private void Start()
    {
        DrawRunnerName();
        DrawHighlight();
    }
    
    public void SetRankSlot(Runner runner)
    {
        Owner = runner;
    }

    void DrawRunnerName()
    {
        _runnerName.text = Owner.RunnerName;
    }
    
    void DrawHighlight()
    {
        if(Owner.CompareTag("Player")) _playerHightlight.SetActive(true);
    }
    
    public void DrawCurrentRank()
    {
        _currentRank.text = CurrentRank.ToString();
    }
}
