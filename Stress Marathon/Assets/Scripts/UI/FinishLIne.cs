using System;
using UnityEngine;
using UnityEngine.UI;

public class FinishLIne : MonoBehaviour
{
    [SerializeField] private LayerMask _entry;
    [SerializeField] GameObject _finish_UI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_entry.Contains(collision.gameObject))
        {
            
            
            
            if (collision.CompareTag("Player"))
            {
                _finish_UI.SetActive(true);
            }
        }

        if (CheakAllRunnerFinish())
        {
            RaceFinish();
        }
    }
    
    private bool CheakAllRunnerFinish()
    {
        return false;
    }

    private void RaceFinish()
    {
        GameManager.IsRacing = false;
    }
}
