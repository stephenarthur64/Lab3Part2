using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    public float speed;
    private EnemySO type;
    public GameManager gmRef;

    public const float LIMIT_RIGHT = 15.0f;
    public const float LIMIT_LEFT = -15.0f;

    private float radius = 5.0f;
    public float angle = 0.0f;
    private Vector3 target;
    private Vector3 transitionalTarget;
    private bool transitionalMove = true;
    private Vector3 startingPosition;
    private float lerpTime;

    public int dropItem;

    public CharacterController controller;

    PowerupManager powerup;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        velocity = new Vector3(speed, 0.0f, 0.0f);

        EnemyManager.changeDirectionLeft.AddListener(ChangeMoveDirectionLeft);
        EnemyManager.changeDirectionRight.AddListener(ChangeMoveDirectionRight); 
    }

    private void OnEnable()
    {
        powerup = GetComponentInChildren<PowerupManager>();
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
                case Movement.Free:
                    FreeMove();
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
        if (transitionalMove)
        {
            transform.position = Vector3.Lerp(startingPosition, transitionalTarget, lerpTime);
            lerpTime += speed * Time.deltaTime;

            if (transform.position == transitionalTarget)
            {
                transitionalMove = false;
            }
            return;
        }
        // x change is target + cos * radius
        // z change is target + sin * radius
        // angle change is speed * time

        Vector3 newPosition = new Vector3(0, 0, 0);

        newPosition.x = target.x + Mathf.Cos(angle) * radius;
        newPosition.y = 0;
        newPosition.z = target.z + Mathf.Sin(angle) * radius;
        angle += speed * Time.deltaTime;

        transform.position = newPosition;
    }

    public void SetTarget(Vector3 t_target)
    {
        startingPosition = t_target;
        transform.position = t_target;
        target = t_target;
    }

    public void InitCircleMovement()
    {
        transitionalTarget.x = target.x + Mathf.Cos(0.0f) * radius;
        transitionalTarget.y = 0;
        transitionalTarget.z = target.z + Mathf.Sin(0.0f) * radius;
        lerpTime = 0.0f;
        speed = 2.0f;
    }

    void FreeMove()
    {
        if (transform.position.z <= target.z)
        {
            lerpTime = 0.0f;
            speed = 0.5f;
            target.x = Random.Range(-10.0f, 10.0f);
            target.z = target.z - 2.0f;
            startingPosition = transform.position;
            return;
        }

        transform.position = Vector3.Lerp(startingPosition, target, lerpTime);
        lerpTime += speed * Time.deltaTime;
    }

    void DieCheck()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log("dead");
            gmRef.ScoreUp(type.modifier);
            if (powerup != null)
            {
                powerup.Spawn();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }

        DieCheck();
    }
}


    