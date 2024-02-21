using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AOE : MonoBehaviour
{
    [SerializeField] float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= 0) {
            Destroy(gameObject);
        }
        lifetime -= Time.deltaTime;
    }
}
