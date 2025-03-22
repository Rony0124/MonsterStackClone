using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private MonsterController monster;
    public void TakeHit()
    {
        monster.CanJump = true;
    }
}
