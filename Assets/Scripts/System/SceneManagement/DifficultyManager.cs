using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    public bool easy, hard;
    public int score;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void DifficultySelect(int Difficulty)
    {
        if(Difficulty == 0)
        {
            easy = true;
            hard = false;
            Debug.Log("easy is true");
        }

        if(Difficulty == 1)
        {
            hard = true;
            easy = false;
            Debug.Log("hard is true");
        }
        score = 0;
    }

    public int Score(int score)
    {
        return this.score = score;
    }
    public void SaveScore()
    {
        score = score;
    }
}
