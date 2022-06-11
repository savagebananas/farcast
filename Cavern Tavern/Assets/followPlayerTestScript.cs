using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerTestScript : MonoBehaviour
{
    Vector2 enemyToPlayerVector;
    public GameObject player;
    float speed = 8f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyToPlayerVector = player.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + (enemyToPlayerVector * speed * Time.deltaTime));
    }
}
