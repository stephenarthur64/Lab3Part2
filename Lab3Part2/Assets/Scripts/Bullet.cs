using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    BulletSO stats;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn(BulletSO t_stats, Transform t_spawnTransform)
    {
        stats = t_stats;
        rb.velocity = transform.forward * stats.speed * Time.deltaTime;

        transform.position = t_spawnTransform.position;
        transform.rotation = t_spawnTransform.rotation;
        gameObject.SetActive(true);
    }
}
