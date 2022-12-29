using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrderInLayer : MonoBehaviour
{
    public SpriteRenderer renderer;
    public float offset;

    private void LateUpdate()
    {
        renderer.sortingOrder = -(int)((transform.position.y + offset) * 500);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + offset, 0), 0.05f);
    }
}
