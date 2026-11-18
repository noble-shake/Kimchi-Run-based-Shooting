using UnityEngine;

public interface IEnemyBullet
{
    public abstract void SetTargetTransform(Transform _trs);
    public abstract void Init();
}
