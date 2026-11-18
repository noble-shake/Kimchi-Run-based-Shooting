using System.Collections;
using UnityEngine;

public interface AttackCommand
{
    public abstract void SetTargetTransform(Transform _trs);

    public abstract IEnumerator EnemyShoot();
}