using UnityEngine;

public class ObjectPoolMonster : ObjectPool<MonsterController>
{
    public override void ReturnObject(MonsterController obj)
    {
        Debug.Log("monster return" + 2);
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

}
