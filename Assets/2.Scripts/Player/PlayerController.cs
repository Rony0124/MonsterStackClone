using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<PlayerBox> onPlayerBoxDead;
    
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
    
    private List<PlayerBox> playerBoxes;
    
    public int damage;
    
    private float shootingTime;

    private void Awake()
    {
        playerBoxes = gameObject.GetComponentsInChildren<PlayerBox>().ToList();
        for (int i = 0; i < playerBoxes.Count; i++)
        {
            playerBoxes[i].SetBox(boxInfos[i], this);
        }

        onPlayerBoxDead += OnPlayerBoxDead;
    }

    private void Update()
    {
        if (shootingTime < Time.time && fov.target != null)
        {
            Shoot();
        }
    }

    private void OnDestroy()
    {
        onPlayerBoxDead = null;
    }

    private void Shoot()
    {
        var bullet = bulletPool.GetObject();
        var newPos = fov.targetDir;
        var rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        bullet.playerController = this;
        
        shootingTime = Time.time + shootingInterval;
    }

    public void RetrieveBullet(PlayerBullet playerBullet)
    {
        bulletPool.ReturnObject(playerBullet);   
    }

    private void OnPlayerBoxDead(PlayerBox box)
    {
        if (playerBoxes.Contains(box))
        {
            var removedY = box.transform.position.y;
            var removedBoxHeight = box.BoxHeight;
            
            playerBoxes.Remove(box);
            
            Destroy(box.gameObject);

            foreach (var otherBox in playerBoxes)
            {
                if (otherBox.transform.position.y > removedY)
                {
                    var newPosition = otherBox.transform.position;
                    newPosition.y -= removedBoxHeight;
                    otherBox.transform.position = newPosition;
                }
            }
        }
    }
}
