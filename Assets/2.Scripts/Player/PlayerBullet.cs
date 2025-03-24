using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float lifeDuration;
    [SerializeField] private float bulletSpeed;

    private float lifeTime;
    
    public PlayerController playerController { get; set; }

    private void OnEnable()
    {
        lifeTime = Time.time + lifeDuration;
    }

    private void FixedUpdate()
    {
        if (Time.time >= lifeTime)
        {
            playerController.RetrieveBullet(this);
            return;
        }
        
        transform.Translate(transform.right * Time.deltaTime * bulletSpeed, Space.World);
    }
}
