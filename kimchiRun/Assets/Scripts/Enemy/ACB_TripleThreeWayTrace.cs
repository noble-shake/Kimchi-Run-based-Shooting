using System;
using System.Collections;
using UnityEngine;

public class ACB_TripleThreeWayTrace : AttackCommand
{
    Transform parenTrs;
    Transform targetTrs;
    public EnemyScript target;

    public Transform SetTarget { get { return targetTrs; } set { targetTrs = value; } }

    public void SetTargetTransform(Transform _trs)
    {
        targetTrs = _trs;
    }

    public ACB_TripleThreeWayTrace(EnemyScript _owner)
    {
        target = _owner;
        parenTrs = _owner.transform;
    }

    public IEnumerator EnemyShoot()
    {
        EnemyBulletA bullet;
        float angle = Quaternion.FromToRotation(Vector3.up, parenTrs.position - targetTrs.position).eulerAngles.z;

        float oneWay = angle - 40f;
        for (int i = 0; i < 8; i++)
        {
            bullet = EnemyManager.Instance.PoolingBA();
            bullet.transform.position = parenTrs.position;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, oneWay));
            oneWay += 10f;
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < 8; i++)
        {
            bullet = EnemyManager.Instance.PoolingBA();
            bullet.transform.position = parenTrs.position;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, oneWay));
            oneWay -= 8f;
            yield return new WaitForSeconds(0.2f);
        }
    }
}