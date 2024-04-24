using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed = 0.05f;
    public float health = 100f;

    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0, 0, speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0, 0, -speed);
        }
    }

    public float TakeDamage(float dmg) 
    {
        health -= dmg;
        Debug.Log("Player: " + health);
        if (health < 0)
        {
            Destroy(gameObject);
        }
        return health;
    }
}
