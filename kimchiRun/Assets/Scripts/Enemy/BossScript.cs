using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum bossState
{ 
    Created,
    SpellReady,
    SpellBreak,
    Pattern1,
    Pattern2,
    Pattern3,
    Dead,
    Wait,
}


public class BossScript : MonoBehaviour
{
    [SerializeField] private bossState bossbt;
    bool bossPattern;

    [SerializeField] private MoveCommand commandMV;
    [SerializeField] private AttackCommand commandAtk;
    [SerializeField] private Transform ChildTrs;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator anim;
    [SerializeField] int MaxHP = 300;
    [SerializeField] int CurHP;

    [SerializeField] float bossTimer;
    [SerializeField] int patternCount = 1;
    public MoveCommand SetMove { get { return commandMV; } set { commandMV = value; } }
    public AttackCommand SetAttack { get { return commandAtk; } set { commandAtk = value; } }

    public int SetHP { get { return MaxHP; } set { MaxHP = value; CurHP = value; } }

    public Sprite SetSprite { set { if (spriteRenderer == null) { spriteRenderer = GetComponentInChildren<SpriteRenderer>(); } spriteRenderer.sprite = value; spriteRenderer.color = Color.white; } }

    public Animator SetAnim { get { if (anim == null) { anim = GetComponentInChildren<Animator>(); } return anim; } }

    IEnumerator shootTrigger;
    public void Shoot()
    {
        if (commandAtk == null) return;
        shootTrigger = commandAtk.EnemyShoot();
        StartCoroutine(shootTrigger);
    }

    private void Update()
    {
        // if (commandAtk == null || commandMV == null) return;


        BehaviourTree();


    }

    private void OnEnable()
    {
        CurHP = MaxHP;
    }
    

    private void BehaviourTree()
    {
        switch (bossbt)
        {
            case bossState.Created:
                StartCoroutine(CreatedBT());
                bossbt = bossState.Wait;
                break;
            case bossState.SpellReady:
                StartCoroutine(SpellReadyBT());
                bossbt = bossState.Wait;
                break;
            case bossState.SpellBreak:
                StartCoroutine(SpellBreakBT());
                bossbt = bossState.Wait;
                break;
            case bossState.Pattern1:
            case bossState.Pattern2:
            case bossState.Pattern3:
                bossTimer -= Time.deltaTime;
                if (bossTimer < 0f)
                {
                    bossTimer = 0f;
                    bossPattern = false;
                    bossbt = bossState.SpellBreak;
                    
                }
                GameManager.Instance.BossTimer.text = bossTimer.ToString();
                if (commandMV == null)
                {
                    return;
                }
                commandMV.EnemyMove();
                break;
            case bossState.Dead:
                StartCoroutine(bossDead());
                bossbt = bossState.Wait;
                break;
            case bossState.Wait:
                break;
        }
    }

    IEnumerator CreatedBT()
    {
        transform.position = new Vector3(0f, 10f, 0f);
        Vector3 source = transform.position;
        float btTime = 0f;
        while (true)
        {
            btTime += Time.deltaTime;
            if (btTime > 1f)
            {
                btTime = 1f;
            }

            transform.position = Vector3.Lerp(source, new Vector3(0f, 4f, 0f), btTime);
            if (Vector2.Distance(transform.position, new Vector3(0f, 4f, 0f)) < 0.001f)
            {
                break;
            }
            yield return null;
        }

        yield return null;
        bossbt = bossState.SpellReady;
    }

    IEnumerator SpellReadyBT()
    {
        GameManager.Instance.BossHPBar.gameObject.SetActive(true);
        GameManager.Instance.BossHPText.gameObject.SetActive(true);
        GameManager.Instance.BossTimer.gameObject.SetActive(true);
        GameManager.Instance.BossTimer.text = 20.ToString();
        float curBarValue = 0f;
        GameManager.Instance.BossHPBar.fillAmount = curBarValue;
        while (curBarValue != 1f)
        {
            curBarValue += Time.deltaTime / 2;
            if (curBarValue > 1f)
            {
                curBarValue = 1f;
            }
            GameManager.Instance.BossHPBar.fillAmount = curBarValue;
            yield return null;
        }
        patternCount++;
        if (patternCount == 1)
        {
            commandMV = new Boss_MCB_Pattern1(this);
            commandAtk = new Boss_ACB_Pattern1(this);
        }
        else if (patternCount == 2)
        {
            commandMV = new Boss_MCB_Pattern2(this);
            commandAtk = new Boss_ACB_Pattern2(this);
        }
        else if (patternCount == 3)
        {
            commandMV = new Boss_MCB_Pattern3(this);
            commandAtk = new Boss_ACB_Pattern3(this);
        }
        yield return new WaitForSeconds(2f);
        

        MaxHP = 450;
        CurHP = 450;
        if (patternCount == 1)
        {
            bossbt = bossState.Pattern1;
        }
        else if (patternCount == 2)
        {
            bossbt = bossState.Pattern2;
        }
        else if (patternCount == 3)
        {
            bossbt = bossState.Pattern3;
        }
        bossPattern = true;
        bossTimer = 24f;
    }

    IEnumerator SpellBreakBT()
    {
        yield return null;
        EnemyManager.Instance.Clear();

        if(shootTrigger !=null) StopCoroutine(shootTrigger);

        commandAtk = null;
        GameManager.Instance.BossHPBar.gameObject.SetActive(false);
        GameManager.Instance.BossHPText.gameObject.SetActive(false);
        GameManager.Instance.BossTimer.gameObject.SetActive(false);
        for (int i = 0; i < 20; i++)
        {
            ItemScript io = ItemManager.Instance.GetItem(true);
            float posX = Random.Range(-3f, 3f);
            float posY = Random.Range(1.5f, 2.5f);
            io.transform.position = transform.position;
            io.InitEffect(posX, posY);
            yield return new WaitForSeconds(0.02f);
        }


        yield return new WaitForSeconds(3f);

        if (patternCount == 3)
        {
            bossbt = bossState.Dead;
        }
        else
        {
            bossbt = bossState.SpellReady;
        }

    }


    private void enemyHit()
    {
        if (bossPattern == true)
        {
            CurHP--;
            if (CurHP <= 0f)
            {

                spriteRenderer.color = Color.white;
                if (Hit != null)
                {
                    StopCoroutine(Hit);
                    Hit = null;
                }
                if (patternCount == 3)
                {
                    EnemyManager.Instance.Clear();

                    if (shootTrigger != null) StopCoroutine(shootTrigger);
                    bossbt = bossState.Dead;
                    return;
                }
                GameManager.Instance.AddScore(10000);
                bossbt = bossState.SpellBreak;
            }
            GameManager.Instance.BossHPBar.fillAmount = ((float)CurHP / (float)MaxHP);

        }
    }

    IEnumerator bossDead()
    {
        GameManager.Instance.BossHPBar.gameObject.SetActive(false);
        GameManager.Instance.BossHPText.gameObject.SetActive(false);
        GameManager.Instance.BossTimer.gameObject.SetActive(false);
       

        for (int i = 0; i < 250; i++)
        {
            ItemScript io = ItemManager.Instance.GetItem(true);
            float posX = Random.Range(-2f, 2f);
            float posY = Random.Range(2.5f, 3.5f);
            io.transform.position = transform.position;
            io.InitEffect(posX, posY);
            yield return new WaitForSeconds(0.02f);
        }

        GameManager.Instance.DeadEnd();
        yield return null;
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManger.GetTag(tags.PlayerBullet)))
        {
            if (CurHP == 0) return;
            if (Hit == null)
            {
                Hit = HitEffect();
                StartCoroutine(Hit);
            }

            if (bossPattern == true)
            {
                enemyHit();
            }

        }
    }

    IEnumerator Hit;

    IEnumerator HitEffect()
    {

        spriteRenderer.color = new Color(1f, 223f/255f, 226f/255f, 1f);

        yield return new WaitForSeconds(0.11f);

        spriteRenderer.color = Color.white;
        Hit = null;
    }
}
