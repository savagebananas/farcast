using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : State
{
    Timer timer;
    private PlayerBase player;

    public GameObject aoePrefab;

    ArrayList activeAOEs;

    public override void OnStart()
    {
        activeAOEs = new ArrayList();
        timer = GetComponent<Timer>();
        player = GameObject.Find("Player").GetComponent<PlayerBase>();
    }

    public override void OnUpdate()
    {
        if (timer.check()) {
            activeAOEs.Add(attack());
        }
    }

    public override void OnLateUpdate()
    {
        
    }

    public GameObject attack() {
        GameObject aoe = Instantiate(aoePrefab, transform);

        aoe.transform.localPosition += new Vector3(Random.Range(-2f,2f),Random.Range(-2f,2f));
        Debug.Log("Spawn");

        return aoe;
    }
}