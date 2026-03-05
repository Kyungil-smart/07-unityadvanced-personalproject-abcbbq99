using System;
using Unity.VisualScripting;
using UnityEngine;

public class Title : MonoBehaviour, IClickAble
{
    [SerializeField] GameObject _Options;
    
    public event Action<IClickAble> OnClickSound;

    private void OnEnable()
    {
        AudioManager.Instance.UIEnableEvents(this);
    }

    private void OnDisable()
    {
        AudioManager.Instance.UIDisableEvents(this);
    }

    public void OnClickStart()
    {
        OnClickSound?.Invoke(this);
        SceneLoader.Instance.ConvertScene(SceneType.Race);
        AudioManager.Instance.PlayRaceBGM();
    }

    public void OnClickOptions()
    {
        OnClickSound?.Invoke(this);
        _Options.SetActive(true);
        gameObject.SetActive(false);
    }
    
}
