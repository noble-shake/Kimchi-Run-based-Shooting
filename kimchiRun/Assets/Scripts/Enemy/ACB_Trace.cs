using System.Collections;
using UnityEngine;

public class ACB_Trace : AttackCommand
{
    Transform parenTrs;
    Transform targetTrs;
    public EnemyScript target;

    public Transform SetTarget { get { return targetTrs; } set { targetTrs = value; } }

    public void SetTargetTransform(Transform _trs)
    { 
        targetTrs = _trs;
    }

    public ACB_Trace(EnemyScript _owner)
    {
        target = _owner;
        parenTrs = _owner.transform;
    }

    public IEnumerator EnemyShoot()
    {
        float angle = Quaternion.FromToRotation(Vector3.up, parenTrs.position - targetTrs.position).eulerAngles.z;
        EnemyBulletA bullet = EnemyManager.Instance.PoolingBA();
        bullet.transform.position = parenTrs.position;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        yield return bullet;
    }
}