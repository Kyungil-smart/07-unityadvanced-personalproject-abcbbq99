using System;
using System.Collections;
using UnityEngine;

public class NPCBackwardZone : MonoBehaviour
{
    [SerializeField] LayerMask _runnerLayer;
    [SerializeField] float _orderTime;
    
    Coroutine _orderCoroutine;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_runnerLayer.Contains(other.gameObject.layer) && other.CompareTag("NPC"))
        {
            NPCController controller = other.gameObject.GetComponent<NPCController>();
            _orderCoroutine = StartCoroutine(BackwardOrderCoroutine(controller));
        }
    }
    
    IEnumerator BackwardOrderCoroutine(NPCController controller)
    {
        if (_orderCoroutine != null) yield break;
        controller.BackwardOrder(_orderTime);
        yield return YieldContainer.WaitForSeconds(_orderTime);
        _orderCoroutine = null;
    }
}