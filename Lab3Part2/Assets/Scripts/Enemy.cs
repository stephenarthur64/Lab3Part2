using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    public float speed;
    private EnemySO type;

    public const float LIMIT_RIGHT = 15.0f;
    public const float LIMIT_LEFT = -15.0f;

    public float radius = 1.0f;
    public float angle = 0.0f;
    private Vector3 target;

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
        if (type != null)
        {
            switch(type.moveType)
            {
                case Movement.Block:
                    BlockMove();
                    break;
                case Movement.Circular:
                    CircularMove();
                    break;
                default:
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        if (type != null)
        {
            switch (type.moveType)
            {
                case Movement.Block:
                    BlockDirectionCheck();
                    break;
                default:
                    break;
            }
        }
    }

    public void setAI(EnemySO t_type)
    {
        type = t_type;
    }

    void BlockMove()
    {
        controller.Move(velocity);
        velocity.z = 0.0f;
    }

    void BlockDirectionCheck()
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


    void CircularMove()
    {
        // x change is target + cos * radius
        // z change is target + sin * radius
        // angle change is speed * time


    }

    void setTarget(Vector3 t_target)
    {
        target = t_target;
    }
}
