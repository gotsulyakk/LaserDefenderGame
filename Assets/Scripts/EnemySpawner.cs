using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping = false;

    WaveConfigSO currentWave;

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {   
                currentWave = wave;
                yield return StartCoroutine(SpawnAllEnemiesInWave(wave));
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfigSO wave)
    {
        for (int i = 0; i < wave.GetEnemyCount(); i++)
        {
            Instantiate(
                wave.GetEnemyPrefab(i),
                wave.GetStartingWaypoint().position,
                Quaternion.Euler(0,0,180),
                transform
            );
            yield return new WaitForSeconds(wave.GetRandomSpawnTime());
        }
    }
}
