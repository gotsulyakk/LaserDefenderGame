using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] int health = 50;
    [SerializeField] bool isPlayer;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = FindObjectOfType<DamageDealer>();
        if (damageDealer)
        {
            TakeDamage(damageDealer.GetDamage());
            audioPlayer.PlayDamageSound();
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(gameObject);
        }
    }

    void Die(GameObject gameObject)
    {
        Destroy(gameObject);
        if (!isPlayer)
        {
            scoreKeeper.AddScore();
        } else
        {
            levelManager.LoadGameOver();
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constant);
        }
    }

    void ShakeCamera()
    {
        if (applyCameraShake && cameraShake != null)
        {
            cameraShake.Shake();
        }
    }
}
