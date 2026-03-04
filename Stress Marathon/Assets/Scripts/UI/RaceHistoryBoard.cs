using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceHistoryBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _1stName;
    [SerializeField] TextMeshProUGUI _2ndName;
    [SerializeField] TextMeshProUGUI _3rdName;
    [SerializeField] GameObject _1stMadel;
    [SerializeField] GameObject _2ndMadel;
    [SerializeField] GameObject _3rdMadel;
    [SerializeField] RaceBoard _raceBoard;

    private void OnEnable()
    {
        Dictionary<int, Runner> finalRanks = _raceBoard.RankHistory;

        for (int i = 1; i <= 3; i++)
        {
            if (finalRanks.TryGetValue(i, out Runner runner))
            {
                TextMeshProUGUI nameBoard;
                GameObject madel;

                switch (i)
                {
                    case 1:
                        nameBoard = _1stName;
                        madel = _1stMadel;
                        break;
                    case 2:
                        nameBoard = _2ndName;
                        madel = _2ndMadel;
                        break;
                    case 3:
                        nameBoard = _3rdName;
                        madel = _3rdMadel;
                        break;
                    default:
                        nameBoard = null;
                        madel = null;
                        break;
                }
                
                if (nameBoard != null) nameBoard.text =  runner != null ? runner.name : "실격";
                if (madel != null && runner != null) madel.SetActive(true);
            }
        }
    }

    public void OnClickToMainMenu()
    {
        SceneLoader.Instance.ConvertScene(SceneType.Title);
        GameManager.Instance.IsRacing = false;
        GameManager.Instance.IsPrized = false;
    }
}
