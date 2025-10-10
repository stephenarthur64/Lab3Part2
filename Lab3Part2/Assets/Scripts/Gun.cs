using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private List<GameObject> bullets = new List<GameObject>();
    private Transform firePos;

    private bool canShoot = true;
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

        firePos = gameObject.GetComponent<Transform>();
    }

    public void Fire(BulletSO t_stats)
    {
        if (canShoot)
        {
            for (int i = 0; i < size; i++)
            {
                if (!bullets[i].activeSelf)
                {
                    Bullet bulletScript = bullets[i].GetComponent<Bullet>();
                    bulletScript.Spawn(t_stats, firePos);
                    StartCoroutine(ShootDelay(t_stats.fireRate));
                    break;
                }
            }
        }
    }

    IEnumerator ShootDelay(float t_fireRate)
    {
        canShoot = false;

        yield return new WaitForSeconds(t_fireRate);

        canShoot = true;
    }
}
