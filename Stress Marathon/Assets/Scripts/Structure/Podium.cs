using System;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    [SerializeField] private Transform _camaraPoint;
    [SerializeField] Transform _1stStand;
    [SerializeField] Transform _2ndStand;
    [SerializeField] Transform _3rdStand;
    [SerializeField] private GameObject _finishUI;

    private Camera _camera;

    private void OnEnable()
    {
        FinishLIne.OnPlayerFinished += SetupPodium;
        _camera = Camera.main;
    }

    private void OnDisable()
    {
        FinishLIne.OnPlayerFinished -= SetupPodium;
    }

    private void Update()
    {
        if(!GameManager.Instance.IsPrized) return;
        _camera.transform.position = _camaraPoint.position;
    }

    private void SetupPodium(Dictionary<int, Runner> finalRanks)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (finalRanks.TryGetValue(i, out Runner runner))
            {
                Transform spot;
                switch (i)
                {
                    case 1:
                        spot = _1stStand;
                        break;
                    case 2:
                        spot = _2ndStand;
                        break;
                    case 3:
                        spot = _3rdStand;
                        break;
                    default:
                        spot = null;
                        break;
                }
                runner.Rb.linearVelocity = Vector2.zero;
                if(spot != null) runner.transform.position = spot.position;
            }
        }
        
        _finishUI.SetActive(true);
    }
}
