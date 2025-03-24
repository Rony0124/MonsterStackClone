using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("Death")]
    [SerializeField] private float deathDuration;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private GroundChecker groundChecker;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private float deathTime;
    private float knockbackTime;
    private float currentKnockbackForce;
    public bool CanJump { get; set; }

    public int Health;
    
    private bool isDead;
    public bool IsDead
    {
        get => isDead;
        set
        {
            if (value)
            {
                OnDead();
            }
            isDead = value;
        }
        
    }
    
    public GroundChecker GroundChecker => groundChecker;
    
    private static readonly int IdleId= Animator.StringToHash("IsAttacking");
    private static readonly int AttackId = Animator.StringToHash("IsDead");
    private static readonly int DeathId = Animator.StringToHash("IsIdle");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead)
        {
            if (deathTime < Time.time)
            {
                GameManager.Instance.MonsterSpawner.pool.ReturnObject(this);
            }
        }
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

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        var floatingText = GameManager.Instance.TextSpawner.pool.GetObject();
        floatingText.transform.position = transform.position;
        floatingText.SetText(damage.ToString());
        
        Health -= damage;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    private void OnDead()
    {
        deathTime = Time.time + deathDuration;
        SetAnimatorParamBool(DeathId, true);
    }
    
    private void SetAnimatorParamBool(int id, bool val)
    {
        if(animator)
        {
            animator.SetBool(id, val);
        }
    }
}
