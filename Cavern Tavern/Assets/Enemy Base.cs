using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject player;

    public float health;
    public float speed;

    public float alertRange;
    public float attackRange;

    private bool isWalking = false;
    public float roamingPointRange;
    private Vector2 nextRoamPosition;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void followPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void wanderAround()
    {
        isWalking = true;

        float randomX = Random.Range(-roamingPointRange, roamingPointRange);
        float randomY = Random.Range(-roamingPointRange, roamingPointRange);
        nextRoamPosition = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

        transform.position = Vector2.MoveTowards(transform.position, nextRoamPosition, speed * Time.deltaTime);
    }
}
