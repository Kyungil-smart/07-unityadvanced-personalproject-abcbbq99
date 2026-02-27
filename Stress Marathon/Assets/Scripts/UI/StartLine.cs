using UnityEngine;
using UnityEngine.Serialization;

public class StartLine : MonoBehaviour
{
    [FormerlySerializedAs("_playerLayer")] [SerializeField] private LayerMask _entry;
    [SerializeField] GameObject _raceBoard;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_entry.Contains(collision.gameObject))
        {
            GameManager.IsRacing = true;
        }
    }
}
