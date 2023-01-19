using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{   
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.25f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    public bool isShooting = false;

    Coroutine shootingCoroutine;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (isShooting && shootingCoroutine == null)
        {
            shootingCoroutine =  StartCoroutine(ShootContinuously());
        }
        else if (!isShooting && shootingCoroutine != null)
        {   
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = transform.up * projectileSpeed;

            audioPlayer.PlayShootingSound();

            Destroy(projectile, projectileLifetime);
            
            if (useAI)
            {
                yield return new WaitForSeconds(GetRandomProjectileSpawnTime());
            }
            else
            {
                yield return new WaitForSeconds(baseFiringRate);
            }
        }
    }

    float GetRandomProjectileSpawnTime()
    {
        float spawnTime = baseFiringRate + Random.Range(-firingRateVariance, firingRateVariance);
        return Mathf.Clamp(spawnTime, minimumFiringRate, float.MaxValue);
    }

}
