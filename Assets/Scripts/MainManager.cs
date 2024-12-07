using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public int aliveEnemyCount = 0;
    public int aliveFlyingEnemyCount = 0;
    public bool gameOver = false;

    public GameObject gameOverScreen;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
      
    }

    void Update()
    {
        
    }
    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    }
}
