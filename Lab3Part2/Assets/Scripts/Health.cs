using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    bool alive = true;
    public int lives = 0;
    public float respawnTime;
    public int maxHealth;
    private int health;

    private bool canTakeDamage = true;
    public float invincibleTime;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }

    public bool IsAlive() {  return alive; }

    public int GetLives() { return lives; }
    public int GetHealth() { return health; }
    public void TakeDamage(int damage)
    {
        if (canTakeDamage && alive) 
        {
            health -= damage;
            StartCoroutine(InvincibilityTimer());

            if (health < 0)
            {
                alive = false;
                lives--;

                if (lives > 0)
                {
                    StartCoroutine(Respawn());
                }
                else
                {
                    alive = false;
                    gameObject.SetActive(false);
                    GameManager.dieSound.Invoke();
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
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        mesh.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnTime);

        health = maxHealth;
        alive = true;
        canTakeDamage = true;
        mesh.gameObject.SetActive(true);
    }

    public void AllowDamage()
    {
        lives = 0;
        alive = true;
        canTakeDamage = true;
    }
}
