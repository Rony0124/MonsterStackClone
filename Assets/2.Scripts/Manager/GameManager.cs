using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action onGameOver;
    
    [SerializeField] private List<MonsterSpawner> monsterSpawners;
    [SerializeField] private FloatingTextSpawner textSpawner;
    [SerializeField] private PlayerController playerController;
    
    public List<MonsterSpawner> MonsterSpawners => monsterSpawners;
    public FloatingTextSpawner TextSpawner => textSpawner;
    public PlayerController PlayerController => playerController;
    
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        foreach (var monsterSpawner in MonsterSpawners)
        {
            monsterSpawner.StopSpawn();
        }

        IsGameOver = true;
    }

    private void OnDestroy()
    {
        onGameOver = null;
    }
}
