using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MCB_RightDown : MoveCommand
{
    float Speed = 2.2f;
    float curTime;
    public Transform targetTransform;
    public EnemyScript owner;
    bool FirstShot;
    bool SeconShot;
    public MCB_RightDown(EnemyScript _owner)
    {
        owner = _owner;
        targetTransform = owner.transform;
    }

    public void EnemyMove()
    {
        curTime += Time.deltaTime;

        if (curTime > 1f && FirstShot == false)
        {
            owner.SetAttack.SetTargetTransform(PlayerScript.Instance.transform);
            owner.Shoot();
            FirstShot = true;

        }

        if (curTime > 2f && SeconShot == false)
        {
            owner.SetAttack.SetTargetTransform(PlayerScript.Instance.transform);
            owner.Shoot();
            SeconShot = true;
        }

        targetTransform.position -= new Vector3(0.005f, Speed * Time.deltaTime * (Mathf.Clamp(curTime, 0.1f, 2f)), 0f);
    }
}