using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public struct PlayerBoxInfo
    {
        public float health;
    }
    
    [Header("Shoot")]
    [SerializeField] private FOV fov;
    [SerializeField] private float shootingInterval;
    [SerializeField] private ObjectPoolBullet bulletPool;
    
    [Header("Box")]
    [SerializeField] private List<PlayerBoxInfo> boxInfos;
    private PlayerBox[] playerBoxes;
    
    public int damage;
    
    private float shootingTime;

    private void Awake()
    {
        playerBoxes = gameObject.GetComponentsInChildren<PlayerBox>();
        for (int i = 0; i < playerBoxes.Length; i++)
        {
            playerBoxes[i].SetBox(boxInfos[i]);
        }
    }

    private void Update()
    {
        if (shootingTime < Time.time && fov.target != null)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = bulletPool.GetObject();
        Vector2 newPos = fov.targetDir;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        bullet.playerController = this;
        
        shootingTime = Time.time + shootingInterval;
    }

    public void RetrieveBullet(PlayerBullet playerBullet)
    {
        bulletPool.ReturnObject(playerBullet);   
    }
}
