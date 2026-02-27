using UnityEngine;
using UnityEngine.Serialization;

public class StartLine : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] GameObject _raceBoard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerLayer.Contains(collision.gameObject))
        {
            _raceBoard.SetActive(true);
        }
    }
}
