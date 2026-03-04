using UnityEngine;

public class FanEffectArea : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;
        
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(box.offset, box.size);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(box.offset, box.size);
    }
}
