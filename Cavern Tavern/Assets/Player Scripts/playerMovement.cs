using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength;
    public float dashCooldown;

    private float dashCounter;
    private float dashCoolCounter;


    private Rigidbody2D characterBody;
    private Vector2 inputMovement;

    public bool isDashing = false;

    void Start()
    {
        activeMoveSpeed = moveSpeed;
        characterBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        inputMovement.Normalize();

        characterBody.velocity = inputMovement * activeMoveSpeed;

        dash();


    }

    void dash()
    {
        if (Input.GetKeyDown("space")) //Initiate Dash
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true;
            }
        }

        if (dashCounter > 0) //During Dash
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <= 0) //when dash duration ended
            {
                activeMoveSpeed = moveSpeed; //normal speed
                dashCoolCounter = dashCooldown; //reset cooldown
                isDashing = false;
            }
        }

        if (dashCoolCounter > 0) //Dash Cooldown
        {
            dashCoolCounter -= Time.deltaTime;
        }

    }
        
}
