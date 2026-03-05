using System;
using UnityEngine;

public class NPCBackwardZone : MonoBehaviour
{
    [SerializeField] LayerMask _runnerLayer;
    [SerializeField] float _orderTime;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_runnerLayer.Contains(other.gameObject.layer) && other.CompareTag("NPC"))
        {
            NPCController controller = other.gameObject.GetComponent<NPCController>();
            if (controller != null) controller.BackwardOrder(_orderTime);
        }
    }
}
