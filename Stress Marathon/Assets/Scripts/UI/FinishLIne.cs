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
            GameManager.IsRacing = false;
            _finish_UI.SetActive(true);
        }
    }
}
