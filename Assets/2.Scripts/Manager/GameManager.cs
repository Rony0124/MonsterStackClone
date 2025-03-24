using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action onGameOver;
    
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private FloatingTextSpawner textSpawner;
    
    public MonsterSpawner MonsterSpawner => monsterSpawner;
    public FloatingTextSpawner TextSpawner => textSpawner;

    private void Awake()
    {
        onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        monsterSpawner.StopSpawn();
    }

    private void OnDestroy()
    {
        onGameOver = null;
    }
}
