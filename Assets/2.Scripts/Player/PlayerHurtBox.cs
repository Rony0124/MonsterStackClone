using UnityEngine;

public class PlayerHurtBox : HurtBox
{
    [SerializeField] private PlayerBox playerBox;
    public override void TakeHit(HitBox hitBox)
    {
        var monsterHitBox = hitBox as MonsterHitBox;
        if (monsterHitBox != null)
        {
            playerBox.TakeDamage(monsterHitBox.GetMonsterDamage());
        }
    }
}
