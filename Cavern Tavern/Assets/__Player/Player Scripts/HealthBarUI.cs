using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerBase playerBase;
    public PlayerMovement playerMovement;

    private float health;
    [HideInInspector] public float lerpTimer;
    private float maxHealth;
    public float chipSpeed;
    public Image frontHealthbar;
    public Image backHealthbar;
    public Color greenColor;
    public Color yellowColor;

    [HideInInspector] public float dashLerpTimer;
    public Image frontDashbar1;
    public Image backDashbar1;
    public Image frontDashbar2;
    public Image backDashbar2;
    private float dashFill = 0f;
    private float regenLength; 
    private float maxDashes; 



    void Start()
    {
        regenLength = playerMovement.dashRegenLength;
        maxDashes = playerMovement.maxDashes;
        health = playerBase.health;
        maxHealth = playerBase.maxHealth;
        dashLerpTimer = 2*regenLength;
        frontDashbar1.enabled = true;
        frontDashbar2.enabled = true;
        backDashbar1.enabled = false;
        backDashbar2.enabled = false;

        //frontHealthbar = Canvas.
        //backHealthbar = GameObject.Find();
        //frontDashbar1 = GameObject.Find();
        //backDashbar1 = GameObject.Find();
        //frontDashbar2 = GameObject.Find();
        //backDashbar2 = GameObject.Find();
}

    void Update()
    {
        UpdateHealthUI();
        UpdateDashUI();
    }

    private void UpdateHealthUI()
    {
        health = playerBase.health;

        float fillF = frontHealthbar.fillAmount;
        float fillB = backHealthbar.fillAmount;
        float healthPercent = health / maxHealth;

        if (fillB > healthPercent) //if player lost health, update ui
        {
            backHealthbar.color = yellowColor;
            frontHealthbar.fillAmount = healthPercent;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthbar.fillAmount = Mathf.Lerp(fillB, healthPercent, percentComplete);
        }
        if (fillB <= healthPercent) //if player gains health, update ui
        {
            backHealthbar.color = greenColor;
            backHealthbar.fillAmount = healthPercent;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthbar.fillAmount = Mathf.Lerp(fillF, backHealthbar.fillAmount, percentComplete);
        }
    }

    private void UpdateDashUI() //Really bad code, needs revamp
    {
        int amountOfDashes = playerMovement.amountOfDashes;
    /*
        float fillF1 = frontDashbar1.fillAmount;
        float fillB1 = backDashbar1.fillAmount;
        float fillF2 = frontDashbar2.fillAmount;
        float fillB2 = backDashbar2.fillAmount;

        float fillAmount = 0;

        if (amountOfDashes == 2)
        {
            frontDashbar1.fillAmount = 1;
            frontDashbar2.fillAmount = 1;
        }
        if (amountOfDashes == 1) 
        {
            frontDashbar1.fillAmount = 1;
            frontDashbar2.fillAmount = 0;
            backDashbar2.enabled = true;
        }

        if (amountOfDashes == 0) 
        {
            frontDashbar1.fillAmount = 0;
            frontDashbar2.fillAmount = 0;
            backDashbar2.enabled = false;
        }
    */
        if(amountOfDashes < maxDashes && dashFill < 2)
        {
            dashLerpTimer += Time.deltaTime;
            dashFill = dashLerpTimer/regenLength;
            if (dashFill >= 1) {
                backDashbar2.fillAmount = dashFill-1f;
                if (!frontDashbar1.enabled)
                backDashbar2.enabled = true;
                backDashbar1.enabled = false;
                frontDashbar1.enabled = true;
                frontDashbar1.fillAmount = 1f;
            }
            if (dashFill < 1) {
                backDashbar1.fillAmount =  dashFill;
                if (frontDashbar2.enabled) {
                    frontDashbar2.enabled = false;

                }
            }

        backDashbar1.fillAmount = Mathf.Min(1, dashFill);
        } else {
            dashFill = 2f;
            if (!frontDashbar2.enabled) {
                frontDashbar2.enabled = true;
                backDashbar2.enabled = false;
                frontDashbar2.fillAmount = 1f;
            }
        }
    }
    public void reduceDash() {
        int numDashes = playerMovement.amountOfDashes;
        dashFill -= 1f;
        dashLerpTimer -= regenLength;
        if (numDashes == 1) {
            frontDashbar2.enabled= false;
            backDashbar2.enabled = true;
            backDashbar2.fillAmount = 0f;
        }
        if (numDashes == 0) {
            frontDashbar1.enabled = false;
            backDashbar1.enabled = true;
            backDashbar1.fillAmount = backDashbar2.fillAmount;
            backDashbar2.fillAmount = 0f;
            backDashbar2.enabled = false;
        }
    }
}
