using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour, IClickAble
{
    [SerializeField] private GameObject _title;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] Slider _mainVolume;
    [SerializeField] Slider _bgmVolume;
    [SerializeField] Slider _sfxVolume;
    
    public event Action<IClickAble> OnClickSound;

    private void OnEnable()
    {
        AudioManager.Instance.UIEnableEvents(this);
    }

    private void OnDisable()
    {
        AudioManager.Instance.UIDisableEvents(this);
    }

    public void SetMainVolume()
    {
        float volume = _mainVolume.value;

        if (volume <= 0.001f)
        {
            _mixer.SetFloat("Master", -80f);
        }
        else
        {
            _mixer.SetFloat("Master", Mathf.Log10(_mainVolume.value) * 20);
        }
    }

    public void SetBgmVolume()
    {
        float volume = _bgmVolume.value;

        if (volume <= 0.001f)
        {
            _mixer.SetFloat("BGM", -80f);
        }
        else
        {
            _mixer.SetFloat("BGM", Mathf.Log10(_bgmVolume.value) * 20);
        }
    }

    public void SetSfxVolume()
    {
        float volume = _sfxVolume.value;

        if (volume <= 0.001f)
        {
            _mixer.SetFloat("SFX", -80f);
        }
        else
        {
            _mixer.SetFloat("SFX", Mathf.Log10(_sfxVolume.value) * 20);
        }
    }
    
    public void OnClickReturn()
    {
        OnClickSound?.Invoke(this);
        _title.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
