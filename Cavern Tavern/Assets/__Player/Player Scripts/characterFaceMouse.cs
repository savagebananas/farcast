using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterFaceMouse : MonoBehaviour
{
    public bool FacingRight  = true;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (TimeManager.isPaused == false)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x < transform.position.x && FacingRight) FlipLeft(); //Face left
            if (mousePos.x > transform.position.x && !FacingRight) FlipRight(); //Face right
        }
    }

    void FlipLeft()
    {
        sprite.flipX = true;
        FacingRight = false;
    }

    void FlipRight()
    {
        sprite.flipX = false;
        FacingRight = true;
    }
}
