using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ACB_Pattern2: AttackCommand
{
    Transform parenTrs;
    Transform targetTrs;
    public BossScript target;

    public Transform SetTarget { get { return targetTrs; } set { targetTrs = value; } }

    public void SetTargetTransform(Transform _trs)
    {
        targetTrs = _trs;
    }

    public Boss_ACB_Pattern2(BossScript _owner)
    {
        target = _owner;
        parenTrs = _owner.transform;
    }

    public IEnumerator EnemyShoot()
    {
        List<EnemyBulletB>  oddBullets = new List<EnemyBulletB>();
        List<EnemyBulletB> evenBullets = new List<EnemyBulletB>();

        List<Vector3> oddVectors = new List<Vector3>();
        List<Vector3> evenVectors = new List<Vector3>();
        
        float LeftAlign = -0.5f;
        float RightAlign = 0f;
        // float downAngle = Quaternion.FromToRotation(Vector3.up, Vector3.down).eulerAngles.z;
        for (int i = 0; i < 10f; i++)
        {

            EnemyBulletB bulletL = EnemyManager.Instance.PoolingBB();
            bulletL.transform.position = new Vector3(LeftAlign, 6.0f, 0f);
            bulletL.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            bulletL.Speed = 0f;
            LeftAlign -= 0.5f;
            EnemyBulletB bulletR = EnemyManager.Instance.PoolingBB();
            bulletR.transform.position = new Vector3(RightAlign, 6.0f, 0f);
            bulletR.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            bulletR.Speed = 0f;
            RightAlign += 0.5f;
            if (i % 2 == 1)
            {
                oddBullets.Add(bulletL);
                oddVectors.Add(bulletL.transform.position);
                oddBullets.Add(bulletR);
                oddVectors.Add(bulletR.transform.position);
            }
            else
            {
                evenBullets.Add(bulletL);
                evenVectors.Add(bulletL.transform.position);
                evenBullets.Add(bulletR);
                evenVectors.Add(bulletR.transform.position);
            }

            yield return new WaitForSeconds(0.055f);
        }

        LeftAlign = 6f;
        RightAlign = 6f;
        float leftToRight = Quaternion.FromToRotation(Vector3.up, Vector3.zero - Vector3.right).eulerAngles.z;
        float rightToLeft = Quaternion.FromToRotation(Vector3.up, Vector3.zero - Vector3.left).eulerAngles.z;
        for (int i = 0; i < 26f; i++)
        {

            EnemyBulletB bulletL = EnemyManager.Instance.PoolingBB();
            bulletL.transform.position = new Vector3(-5f, LeftAlign, 0f);
            bulletL.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, leftToRight));
            bulletL.Speed = 0f;
            LeftAlign -= 0.5f;
            EnemyBulletB bulletR = EnemyManager.Instance.PoolingBB();
            bulletR.transform.position = new Vector3(5f, RightAlign, 0f);
            bulletR.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rightToLeft));
            bulletR.Speed = 0f;
            RightAlign -= 0.5f;
            if (i % 2 == 1)
            {
                oddBullets.Add(bulletL);
                oddBullets.Add(bulletR);
            }
            else
            {
                evenBullets.Add(bulletL);
                evenBullets.Add(bulletR);
            }
            yield return new WaitForSeconds(0.055f);
        }

        yield return new WaitForSeconds(1f);

        Vector3 targetPos = PlayerScript.Instance.transform.position;
        for (int j = 0; j < 3; j++)
        {
            
            for (int i = 0; i < oddVectors.Count; i++)
            {
                EnemyBulletC bulletLeft = EnemyManager.Instance.PoolingBC();
                float oddAngle = Quaternion.FromToRotation(Vector3.up, oddVectors[i] - targetPos).eulerAngles.z;
                bulletLeft.transform.position = oddVectors[i];
                bulletLeft.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, oddAngle));
            }

            yield return new WaitForSeconds(0.075f);
        }

        yield return new WaitForSeconds(0.25f);

        targetPos = PlayerScript.Instance.transform.position;
        for (int j = 0; j < 3; j++)
        {

            for (int i = 0; i < evenVectors.Count; i++)
            {
                EnemyBulletC bulletRight = EnemyManager.Instance.PoolingBC();
                float evenAngle = Quaternion.FromToRotation(Vector3.up, evenVectors[i] - targetPos).eulerAngles.z;
                bulletRight.transform.position = evenVectors[i];
                bulletRight.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, evenAngle));

            }

            yield return new WaitForSeconds(0.075f);
        }

        yield return new WaitForSeconds(1f);

        targetPos = PlayerScript.Instance.transform.position;
        for (int j = 0; j < 3; j++)
        {

            for (int i = 0; i < oddVectors.Count; i++)
            {
                EnemyBulletC bulletLeft = EnemyManager.Instance.PoolingBC();
                float oddAngle = Quaternion.FromToRotation(Vector3.up, oddVectors[i] - targetPos).eulerAngles.z;
                bulletLeft.transform.position = oddVectors[i];
                bulletLeft.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, oddAngle));
            }

            yield return new WaitForSeconds(0.075f);
        }

        yield return new WaitForSeconds(0.25f);

        targetPos = PlayerScript.Instance.transform.position;
        for (int j = 0; j < 3; j++)
        {

            for (int i = 0; i < evenVectors.Count; i++)
            {
                EnemyBulletC bulletRight = EnemyManager.Instance.PoolingBC();
                float evenAngle = Quaternion.FromToRotation(Vector3.up, evenVectors[i] - targetPos).eulerAngles.z;
                bulletRight.transform.position = evenVectors[i];
                bulletRight.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, evenAngle));

            }

            yield return new WaitForSeconds(0.075f);
        }

        foreach (EnemyBulletB b in evenBullets)
        {
            b.Speed = 4f;
        }

        yield return new WaitForSeconds(2f);

        foreach (EnemyBulletB b in oddBullets)
        {
            b.Speed = 6f;
        }

        yield return new WaitForSeconds(2f);

    }
}