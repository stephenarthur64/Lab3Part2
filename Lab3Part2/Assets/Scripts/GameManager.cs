using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI statsText;
    public GameObject playerRef;
    static int score = 0;
    int lives = 0;
    int health = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lives = playerRef.GetComponent<Health>().GetLives();
        health = playerRef.GetComponent<Health>().GetHealth();
        livesText.text = "Lives: " + lives.ToString();
        healthText.text = "Health: " + health.ToString();
        scoreText.text = "Score: " + score.ToString();

        updateStats();
    }

    void updateStats()
    {
        int damage, gunAmount = 0;
        float fireRate, speed, scale = 0.0f;
        damage = playerRef.GetComponent<PlayerMovement>().GetModifiers().damage;
        fireRate = playerRef.GetComponent<PlayerMovement>().GetModifiers().fireRate;
        speed = playerRef.GetComponent<PlayerMovement>().GetModifiers().speed;
        scale = playerRef.GetComponent<PlayerMovement>().GetModifiers().scale;
        gunAmount = playerRef.GetComponent<PlayerMovement>().GetGunAmount();

        statsText.text = "Stats:\nDamage: " + damage.ToString()
            + "\nFire Rate: " + fireRate.ToString()
            + "\nSpeed: " + speed.ToString()
            + "\nScale: " + scale.ToString()
            + "\nGuns: " + gunAmount.ToString();
    }

    public static void ScoreUp(int modifier)
    {
        score += 100 * modifier;
    }
}
