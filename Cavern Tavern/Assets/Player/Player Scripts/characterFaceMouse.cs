using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterFaceMouse : MonoBehaviour
{
    bool FacingRight  = true;

    void Update()
    {
        //flip to direction of cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && FacingRight) { Flip(); }
        if (mousePos.x > transform.position.x && !FacingRight) { Flip(); }
    }

    void Flip()
    {
        Vector3 tmpScale = transform.localScale;
        tmpScale.x = -tmpScale.x;
        transform.localScale = tmpScale;
        FacingRight = !FacingRight;
    }
}
