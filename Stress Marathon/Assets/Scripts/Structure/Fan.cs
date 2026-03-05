using System;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public event Action<Fan> OnFanEnable;
    public event Action<Fan> OnFanDisable;
    
    public AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        AudioManager.Instance.FanEnableEvents(this);
        OnFanEnable?.Invoke(this);
    }

    private void OnDisable()
    {
        OnFanDisable?.Invoke(this);
        AudioManager.Instance.FanDisableEvents(this);
    }
}
