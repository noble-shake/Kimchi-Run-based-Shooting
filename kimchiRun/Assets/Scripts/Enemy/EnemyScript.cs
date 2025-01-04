using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private MoveCommand commandMV;
    [SerializeField] private AttackCommand commandAtk;
    [SerializeField] private Transform ChildTrs;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator anim;
    [SerializeField] int MaxHP = 5;
    [SerializeField] int CurHP;

    public Transform SetChild { get { return ChildTrs; } set { ChildTrs = value; spriteRenderer = ChildTrs.GetComponent<SpriteRenderer>(); spriteRenderer.color = Color.white; } }
    public MoveCommand SetMove { get { return commandMV; } set { commandMV = value; } }
    public AttackCommand SetAttack { get { return commandAtk; } set { commandAtk = value; } }

    public int SetHP { get { return MaxHP; } set { MaxHP = value; CurHP = value; } }

    public Sprite SetSprite { set { if (spriteRenderer == null) { spriteRenderer = GetComponentInChildren<SpriteRenderer>(); } spriteRenderer.sprite = value; spriteRenderer.color = Color.white; } }

    public Animator SetAnim { get { if (anim == null) { anim = GetComponentInChildren<Animator>(); } return anim; } }

    public void Shoot()
    {
        StartCoroutine(commandAtk.EnemyShoot());
    }

    private void Update()
    {
        if (commandAtk == null || commandMV == null) return;

        commandMV.EnemyMove();
        

        if (transform.position.y < -10f || transform.position.x < -8f || transform.position.x > 8f)
        {
            spriteRenderer.color = Color.white;
            transform.SetParent(EnemyManager.Instance.LairEnemy);
            if (Hit != null)
            {
                StopCoroutine(Hit);
                Hit = null;
            }

            Destroy(ChildTrs.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        CurHP = MaxHP;
    }

    private void enemyHit()
    {
        CurHP--;
        if (CurHP <= 0f)
        {
            spriteRenderer.color = Color.white;
            transform.SetParent(EnemyManager.Instance.LairEnemy);
            if (Hit != null)
            {
                StopCoroutine(Hit);
                Hit = null;
            }

            Destroy(ChildTrs.gameObject); 
            gameObject.SetActive(false);
            ItemScript io = ItemManager.Instance.GetItem(false);
            float posX = Random.Range(-0.1f, 0.1f);
            float posY = Random.Range(2.5f, 3.5f);
            io.transform.position = transform.position;
            io.InitEffect(posX, posY);
        }
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
            
            enemyHit();
        }
    }

    IEnumerator Hit;

    IEnumerator HitEffect()
    {

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.11f);

        spriteRenderer.color = Color.white;
        Hit = null;
    }
}
