using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private PlayerBase playerBase;

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

    private Renderer rend;
    private Color characterColor;
    

    void Start()
    {
        playerBase = GetComponent<PlayerBase>();
        characterBody = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();

        activeMoveSpeed = moveSpeed;

        characterColor = rend.material.color;
    }

    void Update()
    {
        movement();
        dash();

        rend.material.color = characterColor;
    }

    void movement()
    {
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        inputMovement.Normalize();
        if (playerBase.isHurt == false)
        {
            characterBody.velocity = inputMovement * activeMoveSpeed; 
        }
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

                Physics2D.IgnoreLayerCollision(9, 10, true);
                characterColor.a = 0.75f; //lower opacity of player
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

                Physics2D.IgnoreLayerCollision(9, 10, false);
                characterColor.a = 1f;
            }
        }

        if (dashCoolCounter > 0) //Dash Cooldown
        {
            dashCoolCounter -= Time.deltaTime;
        }

    }
        
}
