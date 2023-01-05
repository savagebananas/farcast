using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemPosition : MonoBehaviour
{
    public GameObject player;
    private bool facingRight;
    private bool facingLeft;

    public static SetItemPosition instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        setPosition();
        setRotation();
    }

    void setPosition()
    {
        transform.position = player.transform.position;
    }

    void setRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x)
        {
            facingRight = false;
            facingLeft = true;
        }
        if (mousePos.x > transform.position.x)
        {
            facingRight = true;
            facingLeft = false;
        }

        if (facingRight == true)
        {

            Vector3 tmpScale = transform.localScale;
            tmpScale.x = 1;
            transform.localScale = tmpScale;
        }

        if (facingLeft == true)
        {
            Vector3 tmpScale = transform.localScale;
            tmpScale.x = -1;
            transform.localScale = tmpScale;
        }
    }

}
