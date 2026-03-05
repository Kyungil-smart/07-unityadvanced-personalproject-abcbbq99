using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    
    [SerializeField] private AudioSource _mainBGMSource;
    [SerializeField] private AudioSource _mainSfxSource;
    
    [Header("BGM")]
    [SerializeField] private AudioClip _titleBGM;
    [SerializeField] private AudioClip _raceBGM;
    [SerializeField] private AudioClip _prizeBGM;

    [Header("UI")]
    [SerializeField] private AudioClip _clickSfx;
    [SerializeField] private AudioClip _countdownSfx;
    
    [Header("러너")]
    [SerializeField] private AudioClip _jumpSfx;
    [SerializeField] private AudioClip _hitSfx;
    
    [Header("구조물")]
    [SerializeField] private AudioClip _SawSfx;
    [SerializeField] private AudioClip _fanSfx;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayTitleBGM();
    }

    // BGM
    public void MarkerEnableEvents(Marker marker)
    {
        marker.OnBGMPitchChange += SetRaceBGMPitch;
    }

    public void MarkerDisableEvents(Marker marker)
    {
        marker.OnBGMPitchChange -= SetRaceBGMPitch;
    }
    
    private void SetRaceBGMPitch(Marker marker)
    {
        switch (marker.MarkerPosition)
        {
            case 500:
                _mainBGMSource.pitch = 1.1f;
                break;
            case 1000:
                _mainBGMSource.pitch = 1.2f;
                break;
            case 1500:
                _mainBGMSource.pitch = 1.3f;
                break;
            case 2000:
                _mainBGMSource.pitch = 1.4f;
                break;
            case 2500:
                _mainBGMSource.pitch = 1.5f;
                break;
            default:
                _mainBGMSource.pitch = 1.0f;
                break;
        }
    }
    
    public void PlayTitleBGM()
    {
        _mainBGMSource.Stop();
        _mainBGMSource.pitch = 1.0f;
        if(_titleBGM == null) return;
        _mainBGMSource.clip = _titleBGM;
        _mainBGMSource.Play();
    }

    public void PlayRaceBGM()
    {
        _mainBGMSource.Stop();
        _mainBGMSource.pitch = 1.0f;
        if(_raceBGM == null) return;
        _mainBGMSource.clip = _raceBGM;
        _mainBGMSource.Play();
    }

    public void PlayPrizeBGM()
    {
        _mainBGMSource.Stop();
        _mainBGMSource.pitch = 1.0f;
        if(_prizeBGM == null) return;
        _mainBGMSource.clip = _prizeBGM;
        _mainBGMSource.Play();
    }
    
    // UI
    public void UIEnableEvents(IClickAble ui)
    {
        ui.OnClickSound += PlayClickSfx;
    }

    public void UIDisableEvents(IClickAble ui)
    {
        ui.OnClickSound -= PlayClickSfx;
    }
    
    private void PlayClickSfx(IClickAble ui)
    {
        if(_clickSfx == null) return;
        _mainSfxSource.PlayOneShot(_clickSfx);
    }

    public void PlayCountdownSfx()
    {
        if(_countdownSfx == null) return;
        _mainSfxSource.PlayOneShot(_countdownSfx);
    }
        
    // 러너
    public void RunnerEnableEvents(Runner runner)
    {
        runner.OnRunnerHitted += PlayHitSfx;
        runner.OnRunnerJumped += PlayJumpSfx;
        
    }

    public void RunnerDisableEvents(Runner runner)
    {
        runner.OnRunnerHitted -= PlayHitSfx;
        runner.OnRunnerJumped -= PlayJumpSfx;
    }

    private void PlayJumpSfx(Runner runner)
    {
        if(_jumpSfx == null) return;

        if (runner.CompareTag("Player"))
        {
            _mainSfxSource.PlayOneShot(_jumpSfx);
        }
        else
        {
            runner.AudioSource.PlayOneShot(_jumpSfx);
        }
    }

    private void PlayHitSfx(Runner runner)
    {
        if(_hitSfx == null) return;

        if (runner.CompareTag("Player"))
        {
            _mainSfxSource.PlayOneShot(_hitSfx);
        }
        else
        {
            runner.AudioSource.PlayOneShot(_hitSfx);
        }
    }
    
    //구조물
    public void SawEnableEvents(RollingSaw rollingSaw)
    {
        rollingSaw.OnSawCollision += PlaySawSfx;
    }

    public void SawDisableEvents(RollingSaw rollingSaw)
    {
        rollingSaw.OnSawCollision -= PlaySawSfx;
    }
    
    public void FanEnableEvents(Fan fan)
    {
        fan.OnFanEnable += PlayFanSfx;
        fan.OnFanDisable += StopFanSfx;
    }

    public void FanDisableEvents(Fan fan)
    {
        fan.OnFanEnable -= PlayFanSfx;
        fan.OnFanDisable -= StopFanSfx;
    }

    private void PlaySawSfx(RollingSaw rollingSaw)
    {
        if(_SawSfx  == null) return;
        rollingSaw.AudioSource.PlayOneShot(_SawSfx);
    }

    private void PlayFanSfx(Fan fan)
    {
        if(_fanSfx == null) return;
        fan.AudioSource.clip = _fanSfx;
        fan.AudioSource.Play();
    }

    private void StopFanSfx(Fan fan)
    {
        fan.AudioSource.Stop();
    }
}