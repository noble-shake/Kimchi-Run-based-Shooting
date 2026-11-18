using System;
using System.Collections;
using UnityEngine;

public class ACB_CircleSpread: AttackCommand
{
    Transform parenTrs;
    Transform targetTrs;
    public EnemyScript target;

    public Transform SetTarget { get { return targetTrs; } set { targetTrs = value; } }

    public void SetTargetTransform(Transform _trs)
    {
        targetTrs = _trs;
    }

    public ACB_CircleSpread(EnemyScript _owner)
    {
        target = _owner;
        parenTrs = _owner.transform;
    }

    public IEnumerator EnemyShoot()
    {
        EnemyBulletB bullet;
        float angle = Quaternion.FromToRotation(Vector3.up, Vector3.down).eulerAngles.z;

        float oneWay = angle;
        float twoWay = angle;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 48; j++)
            {
                bullet = EnemyManager.Instance.PoolingBB();
                bullet.transform.position = parenTrs.position;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, oneWay));
                oneWay += (360f / 48f);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.2f);
        }


    }
}