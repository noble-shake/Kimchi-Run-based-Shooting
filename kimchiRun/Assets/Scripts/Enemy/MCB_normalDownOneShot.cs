using UnityEngine;

public class MCB_normalDownOneShot : MoveCommand
{
    float Speed = 2.2f;
    float curTime;
    bool FirstShot;
    bool SeconShot;
    public EnemyScript target;
    public Transform targetTransform;

    public MCB_normalDownOneShot(EnemyScript _owner)
    {
        target = _owner;
        targetTransform = target.transform;
    }

    float FireDelay = 0f;

    public void EnemyMove()
    {
        curTime += Time.deltaTime;

        if (curTime < 1.5f)
        {
            targetTransform.position -= new Vector3(0f, Speed * Time.deltaTime / 2f, 0f);

        }
        else if (curTime < 10f)
        {
            FireDelay -= Time.deltaTime;
            if (FireDelay < 0f)
            {
                FireDelay = 0f;
            }

            if (FireDelay == 0f && FirstShot == false)
            {
                target.SetAttack.SetTargetTransform(PlayerScript.Instance.transform);
                target.Shoot();
                FirstShot = true;
                FireDelay = 1.25f;
            }

            targetTransform.position -= new Vector3(0f, Speed * Time.deltaTime / 4f, 0f);
        }
        else
        {
            targetTransform.position -= new Vector3(0f, Speed * Time.deltaTime * (Mathf.Clamp(curTime - 10f, 0.25f, 4f)), 0f);
        }


    }
}