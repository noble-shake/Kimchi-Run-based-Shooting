using System.Collections;
using UnityEngine;

public class Boss_ACB_Pattern1: AttackCommand
{
    Transform parenTrs;
    Transform targetTrs;
    public BossScript target;

    public Transform SetTarget { get { return targetTrs; } set { targetTrs = value; } }

    public void SetTargetTransform(Transform _trs)
    {
        targetTrs = _trs;
    }

    public Boss_ACB_Pattern1(BossScript _owner)
    {
        target = _owner;
        parenTrs = _owner.transform;
    }

    public IEnumerator EnemyShoot()
    {
        float eyeLeftangle = Quaternion.FromToRotation(Vector3.up, new Vector3(-1.25f, 4.0f, 0f) - PlayerScript.Instance.transform.position).eulerAngles.z;
        float eyeRightangle = Quaternion.FromToRotation(Vector3.up, new Vector3(1.25f, 4.0f, 0f) - PlayerScript.Instance.transform.position).eulerAngles.z;

        float leftAngle = eyeLeftangle - 60f;
        float rightAngle = eyeLeftangle + 60f;
        for (int i = 0; i < 3; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(-1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, leftAngle));
            leftAngle += 60f;
        }
        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < 3; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rightAngle));
            rightAngle -= 60f;
        }
        // 2
        leftAngle = eyeLeftangle - 60f;
        rightAngle = eyeLeftangle + 60f;
        for (int i = 0; i < 4; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(-1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, leftAngle));
            leftAngle += 30f;
        }

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 4; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rightAngle));
            rightAngle -= 30f;
        }
        // 3
        leftAngle = eyeLeftangle - 60f;
        rightAngle = eyeLeftangle + 60f;
        for (int i = 0; i < 5; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(-1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, leftAngle));
            leftAngle += 24f;
        }
        for (int i = 0; i < 5; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rightAngle));
            rightAngle -= 24f;
        }

        leftAngle = eyeLeftangle - 60f;
        rightAngle = eyeLeftangle + 60f;
        yield return new WaitForSeconds(0.15f);

        for (int i = 0; i < 8; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(-1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, leftAngle));
            leftAngle += 15f;
        }
        for (int i = 0; i < 8; i++)
        {
            EnemyBulletC bullet = EnemyManager.Instance.PoolingBC();
            bullet.transform.position = new Vector3(1.25f, 4.0f, 0f);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rightAngle));
            rightAngle -= 15f;
        }

        yield return new WaitForSeconds(0.25f);

    }
}