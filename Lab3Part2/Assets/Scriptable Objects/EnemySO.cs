using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Movement
{
    None,
    Block,
    Circular,
    Free
}

[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemySO", order = 2)]


public class EnemySO : ScriptableObject
{
    public Movement moveType;
    public int health;
}
