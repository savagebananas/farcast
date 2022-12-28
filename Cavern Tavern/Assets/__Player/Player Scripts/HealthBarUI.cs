using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerBase playerBase;
    public PlayerMovement playerMovement;

    public float health;
    public float lerpTimer;
    public float maxHealth;
    public float chipSpeed = 3f;
    public Image frontHealthbar;
    public Image backHealthbar;
    public Color greenColor;
    public Color yellowColor;

    public float dashLerpTimer;
    public Image frontDashbar1;
    public Image backDashbar1;
    public Image frontDashbar2;
    public Image backDashbar2;


    void Start()
    {
        health = playerBase.health;
        maxHealth = playerBase.maxHealth;

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
        float maxDashes = playerMovement.maxDashes;
        float amountOfDashes = playerMovement.amountOfDashes;
        float regenLength = playerMovement.dashRegenLength;

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

        if(amountOfDashes < maxDashes)
        {
            dashLerpTimer += Time.deltaTime;
            float percentComplete = dashLerpTimer / regenLength;
            fillAmount = Mathf.Lerp(0f, 1f, percentComplete);
            if (fillAmount == 1) dashLerpTimer = 0f;
        }

        backDashbar1.fillAmount = fillAmount;
        backDashbar2.fillAmount = fillAmount;
    }
}
