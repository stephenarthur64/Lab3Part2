using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    BulletSO stats;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = stats.speed * Time.deltaTime * transform.forward;
    }

    public void Spawn(BulletSO t_stats, Transform t_spawnTransform)
    {
        stats = t_stats;
        transform.SetPositionAndRotation(t_spawnTransform.position, t_spawnTransform.rotation);
        transform.forward = t_spawnTransform.forward;

        gameObject.SetActive(true);
        StartCoroutine(Decay());
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(5);

        gameObject.SetActive(false);
    }
}
