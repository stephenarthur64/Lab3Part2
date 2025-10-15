using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletSO", order = 1)]
public class BulletSO : ScriptableObject
{
    public int damage = 0;
    [Min(0.01f)] public float fireRate = 0;
    public float speed = 0;
    public float scale = 0;
}
