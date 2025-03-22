using UnityEngine;
using UnityEngine.Events;

public class MonsterHurtBox : HurtBox
{
    public UnityEvent onTakeHit;
    
    [SerializeField] private MonsterController monster;
    
    private HitBox hitBox;
    
    public override void TakeHit(HitBox hitBox)
    {
        this.hitBox = hitBox;
        onTakeHit?.Invoke();
    }

    public void MakeMonsterJump()
    {
        monster.CanJump = true;
    }

    public void TakeMonsterDamage()
    {
        var bulletHitBox = hitBox as PlayerBulletHitBox;
        if (bulletHitBox != null) 
            monster.TakeDamage(bulletHitBox.GetBulletDamage());
    }
}
