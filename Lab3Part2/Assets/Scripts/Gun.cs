using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private List<GameObject> bullets = new List<GameObject>();
    private Transform firePos;
    BulletSO bulletStats;

    private bool canShoot = true;
    public int size;

    public bool CanShoot() { return canShoot; }

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
        bulletStats = ScriptableObject.CreateInstance<BulletSO>();
    }

    public void Fire(BulletSO t_originalStats, BulletSO t_modifiers)
    {
        if (canShoot)
        {
            for (int i = 0; i < size; i++)
            {
                if (!bullets[i].activeSelf)
                {
                    Bullet bulletScript = bullets[i].GetComponent<Bullet>();
                    bulletStats.damage = t_originalStats.damage + t_modifiers.damage;
                    bulletStats.fireRate = t_originalStats.fireRate + t_modifiers.fireRate;
                    bulletStats.speed = t_originalStats.speed + t_modifiers.speed;
                    bulletStats.scale = t_originalStats.scale * t_modifiers.scale;

                    bulletScript.Spawn(bulletStats, firePos);
                    StartCoroutine(ShootDelay(bulletStats.fireRate));
                    Debug.Log(t_originalStats.fireRate + " + " + t_modifiers.fireRate);
                    Debug.Log("New fireRate: " + bulletStats.fireRate);
                    break;
                }
            }
        }
    }

    public void Fire(BulletSO t_bulletStats)
    {
        if (canShoot)
        {
            for (int i = 0; i < size; i++)
            {
                if (!bullets[i].activeSelf)
                {
                    Bullet bulletScript = bullets[i].GetComponent<Bullet>();
                    bulletStats.damage = t_bulletStats.damage;
                    bulletStats.fireRate = t_bulletStats.fireRate;
                    bulletStats.speed = t_bulletStats.speed;
                    bulletStats.scale = t_bulletStats.scale;

                    bulletScript.Spawn(bulletStats, firePos);
                    StartCoroutine(ShootDelay(bulletStats.fireRate));
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
