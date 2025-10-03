using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector3 velocity = new Vector3();

    private CharacterController character;

    [SerializeField] private InputActionAsset controls;
    private InputActionMap inputActionMap;

    // Firing
    private BulletPool bulletPool;
    public Transform firePos;
    public BulletSO bulletType;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        bulletPool = GetComponent<BulletPool>();

        inputActionMap = controls.FindActionMap("Player");
        inputActionMap.FindAction("Move").started += OnMove;
        inputActionMap.FindAction("Move").canceled += StopMoving;

        inputActionMap.FindAction("Fire").started += Shoot;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 movementVector = ctx.ReadValue<Vector2>();
        velocity.x = movementVector.normalized.x * speed;
    }

    void StopMoving(InputAction.CallbackContext ctx)
    {
        velocity = new Vector3();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        character.Move(velocity);
    }

    void Shoot(InputAction.CallbackContext ctx)
    {
        bulletPool.Spawn(bulletType, firePos);
    }
}
