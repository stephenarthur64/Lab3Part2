using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    public float speed;

    public const float LIMIT_RIGHT = 15.0f;
    public const float LIMIT_LEFT = -15.0f;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(speed, 0.0f, 0.0f);

        EnemyManager.changeDirectionLeft.AddListener(ChangeMoveDirectionLeft);
        EnemyManager.changeDirectionRight.AddListener(ChangeMoveDirectionRight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (transform.position.x <= LIMIT_LEFT)
        {
            EnemyManager.changeDirectionRight.Invoke();
        }
        else if (transform.position.x >= LIMIT_RIGHT)
        {
            EnemyManager.changeDirectionLeft.Invoke();
        }
    }

    void Move()
    {
        controller.Move(velocity);
        velocity.z = 0.0f;
    }
    void ChangeMoveDirectionLeft()
    {
        velocity.z = -1.0f;
        velocity.x = -speed;
    }
    void ChangeMoveDirectionRight()
    {
        velocity.z = -1.0f;
        velocity.x = speed;
    }
}
