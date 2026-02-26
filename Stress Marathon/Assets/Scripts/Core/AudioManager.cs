using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [SerializeField] private AudioClip _clickClip;
    AudioSource _sfxSource;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if(_clickClip == null) _clickClip = Resources.Load<AudioClip>("sfx_Click");
        
        _sfxSource = GetComponent<AudioSource>();
        if (_sfxSource == null) _sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySfx()
    {
        if(_clickClip == null) return;
        _sfxSource.PlayOneShot(_clickClip);
    }
    
    
    
    
    
    
}