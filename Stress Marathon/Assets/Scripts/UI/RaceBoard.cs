using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceBoard : MonoBehaviour
{
    [SerializeField] private Slider _distanceSlider;
    [SerializeField] TextMeshProUGUI _timerText;
    
    [SerializeField] PlayerController _player;

    private bool _isRacing;

    private float _time;
    
    private void Awake()
    {
        _time = 0f;
    }

    private void OnEnable()
    {
        _isRacing = true;
    }

    private void Update()
    {
        TimerUI();
        SetSlider();
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
        if(!_isRacing) return;
        _time += Time.deltaTime;
    }

    private void SetSlider()
    {
        _distanceSlider.minValue = 0f;
        _distanceSlider.maxValue = 3000f;
        
        _distanceSlider.value = _player.transform.position.x;
    }
}
