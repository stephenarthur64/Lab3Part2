using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private CharacterController characterController;

    private PowerupSO powerupEffect;
    private static PowerupSO[] allPowerups;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Get all Powerups
        allPowerups = Resources.LoadAll<PowerupSO>("ScriptableObjects/PowerupTypes");
        // Grab a SO from the powerup folder
        powerupEffect = allPowerups[Random.Range(0, allPowerups.Length)];

        // Set the powerup's color
        GetComponent<Renderer>().material = powerupEffect.material;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

            player.GainPowerup(powerupEffect);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            Vector3 velocity = new Vector3(0, 0, -speed);
            characterController.Move(velocity);
        }
    }

    public void Spawn()
    {
        transform.parent = null;
        gameObject.SetActive(true);
        Debug.Log("Powerup Spawned!");
    }
}
