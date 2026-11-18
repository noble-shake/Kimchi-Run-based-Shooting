using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ACB_Pattern3: AttackCommand
{
    Transform parenTrs;
    Transform targetTrs;
    public BossScript target;

    public Transform SetTarget { get { return targetTrs; } set { targetTrs = value; } }

    public void SetTargetTransform(Transform _trs)
    {
        targetTrs = _trs;
    }

    public Boss_ACB_Pattern3(BossScript _owner)
    {
        target = _owner;
        parenTrs = _owner.transform;
    }

    public IEnumerator EnemyShoot()
    {
        float curFlow = 0f;
        while (curFlow < 20f)
        {
            curFlow += Time.fixedDeltaTime;
            if (curFlow > 20f)
            {
                curFlow = 20f;
            }

            Vector3 origin = new Vector3(0f, 2f, 0f) + new Vector3(5f * Mathf.Cos(curFlow * 100f), 0.5f * Mathf.Sin(curFlow * 100f), 0f);
            Vector3 realPos1 = origin + new Vector3(2f * Mathf.Cos(curFlow * 20f), 2f * Mathf.Sin(curFlow * 20f), 0f);
            Vector3 realPos2 = origin + new Vector3(2f * -Mathf.Cos(curFlow * 20f), 2f * -Mathf.Sin(curFlow * 20f), 0f);


            EnemyBulletB bullet1 = EnemyManager.Instance.PoolingBB();
            bullet1.Speed = Random.Range(2f, 8f);
            bullet1.transform.position = realPos1;
            bullet1.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            EnemyBulletB bullet2 = EnemyManager.Instance.PoolingBB();
            bullet2.Speed = Random.Range(2f, 8f);
            bullet2.transform.position = realPos2;
            float oddAngle = Quaternion.FromToRotation(Vector3.up, bullet2.transform.position - new Vector3(0f, -2 * Mathf.Sin(curFlow * 10f), 0f)).eulerAngles.z;
            bullet2.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, oddAngle));

            yield return new WaitForSeconds(0.085f);
        }
    }
}