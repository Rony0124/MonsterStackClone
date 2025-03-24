using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Spawner<MonsterController>
{
   [SerializeField] private float spawnInterval;
   [SerializeField] private int orderInLayerOffset;
   
   private float spawnTime;
   
   private const int MaxMonstersCount = 30;

   private void Start()
   {
      spawnTime = Time.time + spawnInterval;
   }

   private void Update()
   {
      if (spawnTime > Time.time)
         return;

      if (poolList.Count >= MaxMonstersCount)
         return;

      var obj = pool.GetObject();
      obj.Graphic.AddOrderInLayer(orderInLayerOffset);
      
      poolList.Add(obj);
      
      spawnTime = Time.time + spawnInterval;
   }
}
