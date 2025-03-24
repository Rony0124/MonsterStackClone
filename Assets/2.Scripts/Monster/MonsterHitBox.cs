using System;
using UnityEngine;

public class MonsterHitBox : HitBox
{
    [SerializeField] private MonsterController monsterController;
    
    private HurtBox hurtBox;

    private void OnCollisionStay2D(Collision2D other)
    {
        if(layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            var hurtBox = other.transform.GetComponent<HurtBox>();
            if (hurtBox != null)
            {
                if (monsterController.CanHit())
                {
                    monsterController.OnHit();
                    this.hurtBox = hurtBox;
                }
            }
        }
    }

    public void OnAttack()
    {
        if(hurtBox != null)
            hurtBox.TakeHit(this);
    }
    
    public float GetMonsterDamage() => monsterController.Damage;

}
