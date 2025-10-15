using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private Rigidbody rb;

    private PowerupSO powerupEffect;
    private static PowerupSO[] allPowerups;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get all Powerups
        allPowerups = Resources.LoadAll<PowerupSO>("ScriptableObjects/PowerupTypes");
        // Grab a SO from the powerup folder
        powerupEffect = allPowerups[Random.Range(0, allPowerups.Length)];

        // Set the powerup's color
        GetComponent<Renderer>().material = powerupEffect.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            player.GainPowerup(powerupEffect);
        }
    }

    public void Spawn()
    {
        transform.parent = null;
        gameObject.SetActive(true);
        Debug.Log("Powerup Spawned!");
    }
}
