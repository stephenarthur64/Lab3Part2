using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletSO", order = 1)]
public class BulletSO : ScriptableObject
{
    public int damage;
    [Min(0.01f)] public float fireRate;
    public float speed;
    public float scale = 1.0f;
}
