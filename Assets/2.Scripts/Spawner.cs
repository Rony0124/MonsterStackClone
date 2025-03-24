using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    public ObjectPool<T> pool;
    
    public List<T> poolList;

    public virtual void StopSpawn()
    {
        foreach (var obj in poolList)
        {
            Destroy(obj.gameObject);
        }
        
        poolList.Clear();
        pool.Clear();
        pool.StopSpawner = true;
    }
}
