using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    private int maxEnemies;
    [SerializeField]
    GameObject[] enemies = new GameObject[1];

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPos = new Vector3(0.0f, 0.0f, 0.0f);

        maxEnemies = enemies.Length;
        for (int i = 0; i < maxEnemies; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation, transform);
            enemies[i].SetActive(false);
            spawnPos.x += 1.5f;
        }

        NewWave();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void NewWave()
    {
        Vector3 newPosition = new Vector3(0, 0, 0);

        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            //enemy.transform.position = newPosition;
            //newPosition.z++;
        }
    }
}
