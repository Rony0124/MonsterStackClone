using System;
using UnityEngine;

public class PlayerBulletHitBox : HitBox
{
    private PlayerBullet playerBullet;

    private void Awake()
    {
        playerBullet = GetComponent<PlayerBullet>();
    }

    public int GetBulletDamage() => playerBullet.playerController.damage;

}
