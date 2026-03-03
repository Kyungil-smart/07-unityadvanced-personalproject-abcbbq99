using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceHistoryBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _1stName;
    [SerializeField] TextMeshProUGUI _2ndName;
    [SerializeField] TextMeshProUGUI _3rdName;
    [SerializeField] RaceBoard _raceBoard;

    private void OnEnable()
    {
        Dictionary<int, Runner> finalRanks = _raceBoard.RankHistory;

        for (int i = 1; i <= 3; i++)
        {
            if (finalRanks.TryGetValue(i, out Runner runner))
            {
                TextMeshProUGUI nameBoard;

                switch (i)
                {
                    case 1:
                        nameBoard = _1stName;
                        break;
                    case 2:
                        nameBoard = _2ndName;
                        break;
                    case 3:
                        nameBoard = _3rdName;
                        break;
                    default:
                        nameBoard = null;
                        break;
                }
                
                if (nameBoard != null) nameBoard.text =  runner != null ? runner.name : "실격";
            }
        }
    }

    private void OnDisable()
    {
        
    }
}
