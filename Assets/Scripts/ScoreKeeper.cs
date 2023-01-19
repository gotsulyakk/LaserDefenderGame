using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{   
    public static ScoreKeeper instance;

    [SerializeField] int initScore = 0;
    [SerializeField] int scorePerEnemy = 10;

    int currentScore = 0;

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

    public int GetCurrentScore()
    {
        return currentScore;
    }

    void Start()
    {
        currentScore = initScore;
    }

    public void AddScore()
    {
        currentScore += scorePerEnemy;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        currentScore = initScore;
    }
}
