using JetBrains.Annotations;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public enum EnemyGroup
{ 
    GrandMother,
    DalBae,
}

[System.Serializable]
public enum BulletGroup
{ 
    TYPEA,
    TYPEB,
    TYPEC,
}


[System.Serializable]
public enum MoveCommandGroup
{ 
    NormalDown,
    RightDown,
    LeftDown,
    NormalDownOneShot,
    BazierDown,
    ZigZagDown,
    SpreadAndDown,
    UpAndDown,
}

[System.Serializable]
public enum AttackCommandGroup
{ 
    Trace,
    ThreeWay,
    TripleTrace,
    TripleThreeWay,
    CircleSpread,
}


public class EnemyManager : MonoBehaviour
{ 
    public static EnemyManager Instance;

    //[SerializeField] EnemyScript GrandMotherPrefab;
    //[SerializeField] EnemyScript DalBaePrefab;

    [SerializeField] GameObject GrandMother;
    [SerializeField] GameObject DalBae;
    [SerializeField] EnemyScript EnemyPrefab;

    private List<EnemyScript> JakoPrefab;

    [SerializeField] EnemyBulletA BulletAPrefab;
    [SerializeField] List<EnemyBulletA> BulletAPool;

    [SerializeField] EnemyBulletB BulletBPrefab;
    [SerializeField] List<EnemyBulletB> BulletBPool;

    [SerializeField] EnemyBulletC BulletCPrefab;
    [SerializeField] List<EnemyBulletC> BulletCPool;

    [SerializeField] BossScript BossPrefab;

    [SerializeField] public Transform LairEnemy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        OnPoolInit();
    }

    public void Clear()
    {
        for (int i = 0; i < BulletAPool.Count; i++)
        {
            BulletAPool[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < BulletBPool.Count; i++)
        {
            BulletBPool[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < BulletCPool.Count; i++)
        {
            BulletCPool[i].gameObject.SetActive(false);
        }
    }

    public void OnPoolInit()
    {
        JakoPrefab = new List<EnemyScript>();

        for (int i = 0; i < 32; i++)
        {
            EnemyScript eo = Instantiate(EnemyPrefab, LairEnemy);
            eo.gameObject.SetActive(false);
            JakoPrefab.Add(eo);
        }

        for (int i = 0; i < 512; i++)
        {
            EnemyBulletA boa = Instantiate(BulletAPrefab, LairEnemy);
            EnemyBulletB bob = Instantiate(BulletBPrefab, LairEnemy);
            EnemyBulletC boc = Instantiate(BulletCPrefab, LairEnemy);
            boa.gameObject.SetActive(false);
            bob.gameObject.SetActive(false);
            boc.gameObject.SetActive(false);
            BulletAPool.Add(boa);
            BulletBPool.Add(bob);
            BulletCPool.Add(boc);
        }
    }

    private EnemyScript GetEnemy()
    {
        foreach (EnemyScript _eo in JakoPrefab)
        {
            if (_eo.gameObject.activeSelf == false)
            {
                return _eo;
            }
        }

        EnemyScript eo = Instantiate(new EnemyScript());
        eo.gameObject.SetActive(false);
        JakoPrefab.Add(eo);
        return JakoPrefab[JakoPrefab.Count];
    }

    public EnemyScript Pooling(EnemyGroup _type)
    {
        EnemyScript eo;
        GameObject renderer;
        switch (_type)
        {
            case EnemyGroup.GrandMother:
                eo = GetEnemy();
                renderer = Instantiate<GameObject>(GrandMother, eo.transform);
                eo.SetChild = renderer.transform;
                eo.transform.SetParent(null);
               
                return eo;
            case EnemyGroup.DalBae:
                eo = GetEnemy();
                renderer = Instantiate<GameObject>(DalBae, eo.transform);
                eo.SetChild = renderer.transform;
                eo.transform.SetParent(null);
                return eo;
            default:
                return null;
        }
    }

    public EnemyBulletA PoolingBA()
    {
        foreach (EnemyBulletA bo in BulletAPool)
        {
            if (bo.gameObject.activeSelf == false)
            { 
                bo.transform.SetParent(null);
                bo.gameObject.SetActive(true);
                return bo;
            }
        }

        EnemyBulletA ins = Instantiate<EnemyBulletA>(BulletAPrefab);
        BulletAPool.Add(ins);
        ins.gameObject.SetActive(true);
        return ins;
    }

    public EnemyBulletB PoolingBB()
    {
        foreach (EnemyBulletB bo in BulletBPool)
        {
            if (bo.gameObject.activeSelf == false)
            {
                bo.transform.SetParent(null);
                bo.gameObject.SetActive(true);
                return bo;
            }
        }

        EnemyBulletB ins = Instantiate<EnemyBulletB>(BulletBPrefab);
        BulletBPool.Add(ins);
        ins.gameObject.SetActive(true);
        return ins;
    }

    public EnemyBulletC PoolingBC()
    {
        foreach (EnemyBulletC bo in BulletCPool)
        {
            if (bo.gameObject.activeSelf == false)
            {
                bo.transform.SetParent(null);
                bo.gameObject.SetActive(true);
                return bo;
            }
        }

        EnemyBulletC ins = Instantiate<EnemyBulletC>(BulletCPrefab);
        BulletCPool.Add(ins);
        ins.gameObject.SetActive(true);
        return ins;
    }

    private MoveCommand GetMoveCommand(EnemyScript _object, MoveCommandGroup _MCG)
    {
        switch (_MCG)
        {
            case MoveCommandGroup.NormalDown:
            default:
                return new MCB_normalDown(_object);
            case MoveCommandGroup.RightDown:
                return new MCB_RightDown(_object);
            case MoveCommandGroup.LeftDown:
                return new MCB_LeftDown(_object);
            case MoveCommandGroup.NormalDownOneShot:
                return new MCB_normalDownOneShot(_object);
        }
    }

    private AttackCommand GetAttackCommand(EnemyScript _object, AttackCommandGroup _AKG)
    {
        switch (_AKG)
        {
            case AttackCommandGroup.Trace:
            default:
                return new ACB_Trace(_object);
            case AttackCommandGroup.ThreeWay:
                return new ACB_ThreeWayTrace(_object);
            case AttackCommandGroup.TripleThreeWay:
                return new ACB_TripleThreeWayTrace(_object);
            case AttackCommandGroup.CircleSpread:
                return new ACB_CircleSpread(_object);
        }
    }

    public void EnemySpawn(EnemyGroup _type, MoveCommandGroup _MCG, AttackCommandGroup _AKG, Vector3 _pos, int _hp = 5)
    {
        EnemyScript eo = Pooling(_type);
        eo.SetHP = _hp;
        eo.transform.position = _pos;
        eo.SetMove = GetMoveCommand(eo, _MCG);
        eo.SetAttack = GetAttackCommand(eo, _AKG);
        eo.gameObject.SetActive(true);
    }

    [SerializeField] float LIMIT_WIDTH_L = -5f;
    [SerializeField] float LIMIT_WIDTH_R = 5f;
    [SerializeField] float LIMIT_HEIGHT_T = 6f;
    [SerializeField] float LIMIT_HEIGHT_B = -6f;

    public Vector3 SpawnAligner(float width = 0.5f, bool height = false, bool side=false)
    {
        float posX = (Mathf.Abs(LIMIT_WIDTH_L) + Mathf.Abs(LIMIT_WIDTH_R)) * width + LIMIT_WIDTH_L;
        float posY = height == false ? 8f : 3.5f;

        return new Vector3(posX, posY, 0f);


    }

    public void BossSpawn()
    {
        BossScript bo = Instantiate<BossScript>(BossPrefab);
        bo.transform.position = new Vector3(0f, 10f, 0f);
    }


}