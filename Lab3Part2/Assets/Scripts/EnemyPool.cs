using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    private const int MAX_ENEMIES = 15;
    GameObject[] enemies = new GameObject[MAX_ENEMIES];

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            Instantiate(enemyPrefab, new Vector3(0, 0, 0), enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
