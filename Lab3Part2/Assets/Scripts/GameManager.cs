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

    private BulletSO bulletStats;

    // Start is called before the first frame update
    void Start()
    {
        bulletStats = ScriptableObject.CreateInstance<BulletSO>();
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
        bulletStats.damage = playerRef.GetComponent<PlayerMovement>().GetStats().damage;
        bulletStats.fireRate = playerRef.GetComponent<PlayerMovement>().GetStats().fireRate;
        bulletStats.speed = playerRef.GetComponent<PlayerMovement>().GetStats().speed;
        bulletStats.scale = playerRef.GetComponent<PlayerMovement>().GetStats().scale;
        int gunAmount = playerRef.GetComponent<PlayerMovement>().GetGunAmount();

        statsText.text = "Stats:\nDamage: " + bulletStats.damage.ToString()
            + "\nFire Rate: " + bulletStats.fireRate.ToString()
            + "\nSpeed: " + bulletStats.speed.ToString()
            + "\nScale: " + bulletStats.scale.ToString()
            + "\nGuns: " + gunAmount.ToString();
    }

    public static void ScoreUp(int modifier)
    {
        score += 100 * modifier;
    }
}
