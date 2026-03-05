using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceHistoryBoard : MonoBehaviour, IClickAble
{
    [SerializeField] TextMeshProUGUI _1stName;
    [SerializeField] TextMeshProUGUI _2ndName;
    [SerializeField] TextMeshProUGUI _3rdName;
    [SerializeField] GameObject _1stMadel;
    [SerializeField] GameObject _2ndMadel;
    [SerializeField] GameObject _3rdMadel;
    [SerializeField] RaceBoard _raceBoard;
    
    public event Action<IClickAble> OnClickSound;

    private void OnEnable()
    {
        AudioManager.Instance.UIEnableEvents(this);
        
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

    private void OnDisable()
    {
        AudioManager.Instance.UIDisableEvents(this);
    }

    public void OnClickToMainMenu()
    {
        OnClickSound?.Invoke(this);
        SceneLoader.Instance.ConvertScene(SceneType.Title);
        AudioManager.Instance.PlayTitleBGM();
        GameManager.Instance.IsRacing = false;
        GameManager.Instance.IsPrized = false;
    }
}
