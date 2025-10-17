using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int lives = 0;
    public int maxHealth;
    private int health;

    private bool canTakeDamage = true;
    public float invincibleTime;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }

    public int GetLives() { return lives; }
    public int GetHealth() { return health; }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage) 
        {
            health -= damage;
            StartCoroutine(InvincibilityTimer());

            if (health < 0)
            {
                gameObject.SetActive(false);

                if (lives > 0)
                {
                    StartCoroutine(Respawn());
                }
            }
        }
    }

    public void Heal(int healAmount) 
    { 
        health += healAmount;
    }

    IEnumerator InvincibilityTimer()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(invincibleTime);

        canTakeDamage = true;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(invincibleTime);

        health = maxHealth;
        gameObject.SetActive(true);
        canTakeDamage = true;
    }

    public void AllowDamage()
    {
        canTakeDamage = true;
    }
}
