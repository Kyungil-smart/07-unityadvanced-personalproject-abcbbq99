using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class RaceBoard : MonoBehaviour
{
    [SerializeField] private Slider _distanceSlider;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] GameObject _rankSlotPrefab;
    [SerializeField] Transform _RankingBoardContent;
    
    [SerializeField] PlayerController _player;

    private List<Runner> _entryList;
    private Dictionary<Runner, RankSlot> _rankTable;
    
    private float _time;
    
    private void Awake()
    {
        _time = 0f;
        _entryList = new List<Runner>();
        _rankTable = new Dictionary<Runner, RankSlot>();
    }

    private void Update()
    {
        TimerUI();
        SetSlider();
        SortRunners();
    }
    
    private void TimerUI()
    {
        RunTimer();
        
        int minutes = (int)(_time / 60);
        int seconds = (int)(_time % 60);
        int microsecond = (int)((_time * 100) % 100);

        _timerText.text = string.Format("{0:D2} : {1:D2} : {2:D2}", minutes, seconds, microsecond);
    }

    private void RunTimer()
    {
        if(!GameManager.IsRacing) return;
        _time += Time.deltaTime;
    }

    private void SetSlider()
    {
        _distanceSlider.minValue = 0f;
        _distanceSlider.maxValue = 3000f;
        
        _distanceSlider.value = _player.transform.position.x;
    }

    public void AddRunner(Runner runner)
    {
        _entryList.Add(runner);
        GameObject newRankSlot = Instantiate(_rankSlotPrefab, _RankingBoardContent);
        RankSlot rankSlot = newRankSlot.GetComponent<RankSlot>();
        rankSlot.SetRankSlot(runner);
        _rankTable.Add(runner ,rankSlot);
    }

    void SortRunners()
    {
        _entryList.Sort((a, b) 
            => b.transform.position.x.CompareTo(a.transform.position.x));

        for (int i = 0; i < _entryList.Count; i++)
        {
            Runner currentRunner = _entryList[i];
            RankSlot slot = _rankTable[currentRunner];

            slot.CurrentRank = i + 1;
            slot.DrawCurrentRank();

            slot.transform.SetSiblingIndex(i);
        }
    }
}
