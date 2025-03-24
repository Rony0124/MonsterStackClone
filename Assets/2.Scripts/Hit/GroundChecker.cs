using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask layerMask;
    
    public bool IsGrounded { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsGrounded = true;
            }
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var monster = other.transform.GetComponent<MonsterController>();
                monster.KnockBack();
            }
        }
    }
    
    
}
