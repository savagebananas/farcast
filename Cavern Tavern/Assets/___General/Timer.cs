using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float cooldown;

    private float curTime;

    public Timer(float cooldown) {
        this.cooldown = cooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        curTime = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime > 0) {
            curTime -= Time.deltaTime;
        }
    }

    public void restart() {
        curTime = cooldown;
    }

    public void setCooldown(float cooldown) {
        this.cooldown = cooldown;
    }

    public bool check() {
        if (curTime <= 0) {
            curTime = cooldown;
            return true;
        }
        return false;
    }
}
