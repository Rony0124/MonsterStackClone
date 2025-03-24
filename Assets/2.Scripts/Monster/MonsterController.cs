using System;
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
    
    [Header("Hit")]
    [SerializeField] private float monsterHitInterval;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float attackAnimationDuration;

    private Rigidbody2D rb;
    private float deathTime;
    private float knockbackTime;
    private float currentKnockbackForce;
    private float monsterHitTime;
    private float attackAnimationTime;
    public bool CanJump { get; set; }

    [Header("Stat")]
    public float Health;
    public float Damage;
    
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
    
    private static readonly int IdleId= Animator.StringToHash("IsIdle");
    private static readonly int AttackId = Animator.StringToHash("IsAttacking");
    private static readonly int DeathId = Animator.StringToHash("IsDead");

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
                SetAnimatorParamBool(DeathId, false);
                GameManager.Instance.MonsterSpawner.pool.ReturnObject(this);
                isDead = false;
            }
        }

        if (!isDead)
        {
            if (attackAnimationTime < Time.time)
            {
                SetAnimatorParamBool(AttackId, false);
            }
        }
    }

    private void FixedUpdate()
    {
        if(isDead)
            return;
        
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
        var floatingText = GameManager.Instance.TextSpawner.pool.GetObject();
        floatingText.transform.position = transform.position;
        floatingText.SetText(damage.ToString());
        
        Health -= damage;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public void OnHit()
    {
        monsterHitTime = Time.time + monsterHitInterval;
        attackAnimationTime = Time.time + attackAnimationDuration;
        
        SetAnimatorParamBool(AttackId, true);
    }

    private void OnDead()
    {
        deathTime = Time.time + deathDuration;
        SetAnimatorParamBool(DeathId, true);
        rb.velocity = Vector2.zero;
    }
    
    private void SetAnimatorParamBool(int id, bool val)
    {
        if(animator)
        {
            animator.SetBool(id, val);
        }
    }
    
    public bool CanHit() => monsterHitTime < Time.time;
}
