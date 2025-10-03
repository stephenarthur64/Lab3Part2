using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    public float speed;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(speed, 0.0f, 0.0f);

        EnemyManager.changeDirectionLeft.AddListener(ChangeMoveDirection);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(10.0f, transform.position.y, transform.position.z);
            EnemyManager.changeDirectionLeft.Invoke();
        }
        else if (transform.position.x > 10)
        {
            transform.position = new Vector3(-10.0f, transform.position.y, transform.position.z);
            EnemyManager.changeDirectionLeft.Invoke();
        }

        controller.Move(velocity);
        velocity.z = 0.0f;
    }
    void ChangeMoveDirection()
    {
        velocity.x = velocity.x * -1;
    }
}
