using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingSound;
    [SerializeField] [Range(0, 1)] float shootingSoundVolume = 0.5f;

    [Header("Damage")]
    [SerializeField] AudioClip damageSound;
    [SerializeField] [Range(0, 1)] float damageSoundVolume = 0.5f;
    
    public static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } 
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void PlayShootingSound()
    {
        PlayClip(shootingSound, shootingSoundVolume);
    }

    public void PlayDamageSound()
    {
        PlayClip(damageSound, damageSoundVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPosition, volume);
        }
    }
}
