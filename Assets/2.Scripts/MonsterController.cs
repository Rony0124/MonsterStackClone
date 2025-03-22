using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private FOV fov;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackDuration;
    
    [SerializeField] private GroundChecker groundChecker;

    private Rigidbody2D rb;
    private float knockbackTime;
    private float currentKnockbackForce;
    public bool CanJump { get; set; }
    
    public GroundChecker GroundChecker => groundChecker;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float xMoveSpeed = moveSpeed;
        float yMoveSpeed = rb.velocity.y;
        
        if (knockbackTime > Time.time)
        {
            currentKnockbackForce -= currentKnockbackForce * Time.fixedDeltaTime;
            xMoveSpeed -= currentKnockbackForce;
        }
        else
        {
            if (CanJump)
            {
                JumpOnBack();
                CanJump = false;
            }
        }

        if (rb.velocity.y > jumpForce)
        {
            yMoveSpeed = jumpForce;
        }
        
        rb.velocity = new Vector2(-xMoveSpeed, yMoveSpeed);
    }

    public void JumpOnBack()
    {
        rb.AddForce(jumpForce * (Vector2.up), ForceMode2D.Impulse);
    }

    public void KnockBack()
    {
        knockbackTime = Time.time + knockbackDuration;
        currentKnockbackForce = moveSpeed + knockbackForce;
    }
}
