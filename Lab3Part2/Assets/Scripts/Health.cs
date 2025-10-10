using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int health;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Done: " +  damage);

        if (health < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Heal(int healAmount) 
    { 
        health += healAmount;
    }

}
