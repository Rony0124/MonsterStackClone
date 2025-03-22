using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
   [SerializeField] 
   private GameObject monsterPrefab;
   
   [SerializeField]
   private float spawnInterval;

   private float spawnTime;
   
   private List<GameObject> monsterList = new List<GameObject>();
   
   private const int MaxMonstersCount = 10;

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
      
      var obj = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
      monsterList.Add(obj);
   }
}
