using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerBase playerBase;
    public HealthBarUI healthBarUI;
    private Rigidbody2D characterBody;
    private Vector2 inputMovement;

    public float moveSpeed;
    public float dashSpeed;
    public float dashLength;
    public float maxDashes = 2;
    public float amountOfDashes;
    public float dashRegenLength;
    private float nextRegenTime;
    public bool isDashing = false;
    
    private float activeMoveSpeed;
    
    void Start()
    {
        playerBase = GetComponent<PlayerBase>();
        characterBody = GetComponent<Rigidbody2D>();

        activeMoveSpeed = moveSpeed;

        amountOfDashes = maxDashes;
    }

    void Update()
    {
        Movement(); 
        RegenerateDash();

        if (Input.GetKeyDown("space") && amountOfDashes > 0 && 
            (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))) //space pressed and movement key pressed
        {
            StartCoroutine(dash());
            nextRegenTime = Time.time + dashRegenLength;
        }
    }

    void Movement()
    {
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        inputMovement.Normalize();
        if (playerBase.isHurt == false)
        {
            characterBody.velocity = inputMovement * activeMoveSpeed; 
        }
    }

    IEnumerator dash()
    {
        activeMoveSpeed = dashSpeed;
        isDashing = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        amountOfDashes -= 1;
        healthBarUI.dashLerpTimer = 0f;

        yield return new WaitForSeconds(dashLength);

        activeMoveSpeed = moveSpeed; //normal speed
        isDashing = false;

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    void RegenerateDash()
    {
        if (amountOfDashes < maxDashes)
        {
            if (Time.time > nextRegenTime)
            {
                nextRegenTime = Time.time + dashRegenLength;
                amountOfDashes++;
            }
        }
    }
}
