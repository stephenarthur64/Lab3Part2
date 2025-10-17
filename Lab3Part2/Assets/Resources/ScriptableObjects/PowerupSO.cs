using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupType", menuName = "ScriptableObjects/PowerupSO", order = 3)]
public class PowerupSO : ScriptableObject
{
    // Powerup Color
    public Material material;

    // Guns
    public float fireRateChange;
    [Min(0)] public int addGuns;
    public GameObject gunTypePrefab = null;

    // Bullet
    public int damageChange;
    [Min(0.1f)] public float scaleMult = 1;

    // Player
    public int healthChange;
}
