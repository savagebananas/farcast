using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private PlayerBase playerBase;
    private Rigidbody2D characterBody;
    private Vector2 inputMovement;
    private Renderer rend;
    private Color characterColor;

    public float moveSpeed;
    public float dashSpeed;
    public float dashLength;
    public float amountOfDashes;
    public bool isDashing = false;

    
    private float activeMoveSpeed;
    
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
        if (Input.GetKeyDown("space") && amountOfDashes > 0) StartCoroutine(dash());
        movement();

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


    IEnumerator dash()
    {
        activeMoveSpeed = dashSpeed;
        isDashing = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        characterColor.a = 0.6f;
        amountOfDashes -= 1;


        yield return new WaitForSeconds(dashLength);

        activeMoveSpeed = moveSpeed; //normal speed
        isDashing = false;

        Physics2D.IgnoreLayerCollision(9, 10, false);
        characterColor.a = 1f;
    }
        
}
