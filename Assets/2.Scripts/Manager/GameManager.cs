using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private FloatingTextSpawner textSpawner;
    
    public MonsterSpawner MonsterSpawner => monsterSpawner;
    public FloatingTextSpawner TextSpawner => textSpawner;
}
