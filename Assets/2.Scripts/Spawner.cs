using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    public ObjectPool<T> pool;
    
    public List<T> poolList;

    public void StopSpawn()
    {
        pool.StopSpawner = true;
    }
}
