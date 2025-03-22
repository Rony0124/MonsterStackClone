using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
   public ObjectPoolMonster monsterPool;
   
   [SerializeField]
   private float spawnInterval;

   private float spawnTime;
   
   private List<MonsterController> monsterList = new List<MonsterController>();
   
   private const int MaxMonstersCount = 30;

   private void Start()
   {
      spawnTime = Time.time + spawnInterval;
   }

   private void Update()
   {
      if (spawnTime > Time.time)
         return;

      if (monsterList.Count >= MaxMonstersCount)
         return;

      var obj = monsterPool.GetObject();
      
      monsterList.Add(obj);
      
      spawnTime = Time.time + spawnInterval;
   }
}
