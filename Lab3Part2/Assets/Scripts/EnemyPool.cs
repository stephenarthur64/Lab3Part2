using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public EnemySO blockAI;
    public EnemySO circleAI;
    public EnemySO freeAI;

    int waveCount = 0;
    private int activeCount;
    private bool waveReady = true;

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private int maxEnemies;
    [SerializeField]
    GameObject[] enemies = new GameObject[1];

    public GameObject CircleTarget;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPos = new Vector3(0.0f, 0.0f, 2.0f);

        maxEnemies = enemies.Length;
        for (int i = 0; i < maxEnemies; i++)
        {
            if (i % 5 == 0 && i > 1)
            {
                spawnPos.z += 1.5f;
                spawnPos.x = 0.0f;
            }
            enemies[i] = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation, transform);
            SpawnPowerUp(enemies[i]);
            enemies[i].SetActive(false);

            spawnPos.x += 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeSelf)
            {
                activeCount++;
            }
        }
        if (activeCount == maxEnemies && waveReady)
        {
            waveReady = false;
            NewWave();
        }
        activeCount = 0;
    }
    
    IEnumerator SpawnDelay()
    {
        foreach (GameObject enemy in enemies)
        {
            yield return new WaitForSeconds(1.0f);
            waveReady = true;
            enemy.SetActive(true);
        }
    }


    void SpawnPowerUp(GameObject t_enemy)
    {
        if (Random.Range(0, 4) == 0)
        {
            Instantiate(powerupPrefab, t_enemy.transform);
        }
    }

    void NewWave()
    {
        waveCount++;

        Vector3 newPosition = new Vector3(0, 0, 10.0f);

        if (waveCount == 1)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().setAI(blockAI);
                enemy.SetActive(true);
                SpawnPowerUp(enemy);
                waveReady = true;
            }
        }

        if (waveCount == 2)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().setAI(circleAI);
                enemy.GetComponent<Enemy>().SetTarget(CircleTarget.transform.position);
                enemy.GetComponent<Enemy>().InitCircleMovement();
                SpawnPowerUp(enemy);
            }

            StartCoroutine(SpawnDelay());
        }

        if (waveCount == 3)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().setAI(freeAI);
                enemy.GetComponent<Enemy>().SetTarget(newPosition);
                SpawnPowerUp(enemy);
            }

            StartCoroutine(SpawnDelay());
        }
    }
}
