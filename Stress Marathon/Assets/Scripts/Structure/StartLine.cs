using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class StartLine : MonoBehaviour
{
    [SerializeField] private LayerMask _entry;

    private void OnEnable()
    {
        StartCoroutine(RaceStartCoroutine());
    }
    
    IEnumerator RaceStartCoroutine()
    {
        AudioManager.Instance.PlayCountdownSfx();
        yield return YieldContainer.WaitForSeconds(3f);
        GameManager.Instance.IsRacing = true;
    }
}
