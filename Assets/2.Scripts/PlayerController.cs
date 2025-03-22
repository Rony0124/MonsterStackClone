using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FOV fov;
    [SerializeField] private float shootingInterval;
    [SerializeField] private ObjectPoolBullet bulletPool;
    
    public int damage;
    
    private float shootingTime;
    
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
        bullet.transform.rotation = fov.transform.rotation;
        bullet.playerController = this;
        
        shootingTime = Time.time + shootingInterval;
        Debug.Log("Shoot");
    }

    public void RetrieveBullet(PlayerBullet playerBullet)
    {
        bulletPool.ReturnObject(playerBullet);   
    }
}
