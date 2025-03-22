using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MonsterSpawner monsterSpawner;
    
    public MonsterSpawner MonsterSpawner => monsterSpawner;
}
