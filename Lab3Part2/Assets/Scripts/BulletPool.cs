using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    private List<GameObject> bullets = new List<GameObject>();

    public int size;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullets.Add(bullet);
            bullet.SetActive(false);
        }
    }

    public void Spawn(BulletSO t_stats, Transform t_spawnTransform)
    {
        for (int i = 0; i < size; i++)
        {
            if (!bullets[i].activeSelf)
            {
                Bullet bulletScript = bullets[i].GetComponent<Bullet>();
                bulletScript.Spawn(t_stats, t_spawnTransform);
                break;
            }
        }
    }
}
