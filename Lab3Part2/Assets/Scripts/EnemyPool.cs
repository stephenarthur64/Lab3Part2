using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public EnemySO blockAI;
    public EnemySO circleAI;

    int waveCount = 1;

    public GameObject enemyPrefab;
    private int maxEnemies;
    [SerializeField]
    GameObject[] enemies = new GameObject[1];

    public GameObject CircleTarget;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPos = new Vector3(0.0f, 0.0f, 0.0f);

        maxEnemies = enemies.Length;
        for (int i = 0; i < maxEnemies; i++)
        {
            if (i % 5 == 0 && i > 1)
            {
                spawnPos.z += 1.5f;
                spawnPos.x = 0.0f;
            }
            enemies[i] = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation, transform);
            enemies[i].SetActive(false);

            spawnPos.x += 2.0f;
        }

        NewWave();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    IEnumerator SpawnDelay()
    {
        foreach (GameObject enemy in enemies)
        {
            yield return new WaitForSeconds(1.0f);
            enemy.SetActive(true);
        }
    }

    void NewWave()
    {
        waveCount++;

        Vector3 newPosition = new Vector3(0, 0, 0);

        if (waveCount == 1)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().setAI(blockAI);
                enemy.SetActive(true);
            }
        }

        if (waveCount == 2)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().setAI(circleAI);
                enemy.GetComponent<Enemy>().setTarget(CircleTarget.transform.position);
            }

            StartCoroutine(SpawnDelay());
        }
    }
}
