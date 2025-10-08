using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    InputAction fireAction;
    public GameObject gunPrefab;
    float gunSpread = 35f;
    private Gun[] guns;
    public BulletSO bulletType;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        SetupGuns();

        inputActionMap = controls.FindActionMap("Player");
        inputActionMap.FindAction("Move").started += OnMove;
        inputActionMap.FindAction("Move").canceled += StopMoving;

        fireAction = inputActionMap.FindAction("Fire");
        fireAction.Enable();
    }

    void SetupGuns()
    {
        guns = GetComponentsInChildren<Gun>();
        SetGunRotations();
    }

    void SetGunRotations()
    {
        int count = guns.Length;

        if (count <= 1)
        {
            // Only one gun, set it to center angle
            guns[0].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            float angleBetween = gunSpread / (count - 1);
            float startAngle = -gunSpread / 2f;

            for (int i = 0; i < count; i++)
            {
                float currentAngle = startAngle + angleBetween * i;
                guns[i].transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
            }
        }
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

        if (fireAction.IsPressed())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        foreach (Gun gun in guns)
        {
            gun.Fire(bulletType);
        }
    }
}
