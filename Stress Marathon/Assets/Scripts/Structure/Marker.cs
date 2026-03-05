using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Marker : MonoBehaviour
{
    [SerializeField] LayerMask _runnerLayer;
    [SerializeField] Transform _summonPoint;
    [SerializeField] RaceBoard _board;
    [SerializeField] float _summonDelayTime;
    
    public event Action<Marker> OnBGMPitchChange;
    Coroutine _summonCoroutine;
    
    public int MarkerPosition { get; private set; }

    private void Awake()
    {
        MarkerPosition = (int)transform.position.x;
        _summonCoroutine = null;
    }

    private void OnEnable()
    {
        AudioManager.Instance.MarkerEnableEvents(this);
    }

    private void OnDisable()
    {
        AudioManager.Instance.MarkerDisableEvents(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_runnerLayer.Contains(other.gameObject.layer) && other.CompareTag("Player"))
        {
            OnBGMPitchChange?.Invoke(this);
            _summonCoroutine = StartCoroutine(SummonCoroutine());
        }
    }

    IEnumerator SummonCoroutine()
    {
        if (_summonCoroutine != null) yield break;
        yield return YieldContainer.WaitForSeconds(_summonDelayTime);
        List<Runner> runners = _board.EntryList;
        for (int i = 0; i < runners.Count; i++)
        {
            if (runners[i].transform.position.x < MarkerPosition)
            {
                if (runners[i].TryGetComponent(out NPCController npc))
                {
                    npc.transform.position = new Vector3((_summonPoint.position.x - Random.Range(-3f,3f)), _summonPoint.position.y, _summonPoint.position.z);
                    npc.StopCoroutine(npc.ChangeMoveSpeedCoroutine());
                    npc.CurrentSpartCoroutine = StartCoroutine(npc.SpartCoroutine());
                }
            }
        }
        
        _summonCoroutine = null;
    }
}
