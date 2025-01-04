using UnityEngine;

public class Boss_MCB_Pattern3: MoveCommand
{
    float Speed = 2.2f;
    float curTime;
    bool FirstShot;
    bool SeconShot;
    public BossScript parentObject;
    public Transform parentTransform;

    public Boss_MCB_Pattern3(BossScript _owner)
    {
        parentObject = _owner;
        parentTransform = parentObject.transform;
        curTime = 0f;
    }

    float FireDelay = 0f;

    public void EnemyMove()
    {
        if (FirstShot == false)
        {
            parentObject.Shoot();
            FirstShot = true;
        }
    }
}