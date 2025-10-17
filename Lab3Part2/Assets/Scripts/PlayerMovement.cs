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
    [SerializeField] private BulletSO bulletModifiers;

    // Health
    Health health;

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

        health = GetComponent<Health>();

        bulletModifiers = ScriptableObject.CreateInstance<BulletSO>();
        bulletModifiers.scale = 1.0f;
    }

    public BulletSO GetStats() 
    {
        BulletSO combinedStats = ScriptableObject.CreateInstance<BulletSO>();
        combinedStats.damage = bulletType.damage + bulletModifiers.damage;
        combinedStats.fireRate = bulletType.fireRate + bulletModifiers.fireRate;
        combinedStats.speed = bulletType.speed + bulletModifiers.speed;
        combinedStats.scale = bulletType.scale * bulletModifiers.scale;

        return combinedStats; 
    }
    public int GetGunAmount() { return guns.Length; }

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
            gun.Fire(bulletType, bulletModifiers);
        }
    }

    public void GainPowerup(PowerupSO t_powerup)
    {
        health.Heal(t_powerup.healthChange);

        bulletModifiers.scale *= t_powerup.scaleMult;
        bulletModifiers.damage += t_powerup.damageChange;
        bulletModifiers.fireRate += t_powerup.fireRateChange;

        // Add guns
        if (t_powerup.addGuns > 0)
        {
            for (int i = 0; i < t_powerup.addGuns; i++)
            {
                Instantiate(t_powerup.gunTypePrefab, transform);
            }

            SetupGuns();
        }
    }
}
