using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public BulletScriptPlayer NormalBullet;
    public BulletScriptPlayerSub PowerBullet;
    public SpriteRenderer spriteRenderer;

    public int NormalProjectiles = 2;
    public float NormalBulletDelay = 0.155f;
    public float CurNormalBulleDelay;

    public int powerStack = 0;
    public int PowerProjectiles = 2;
    public float OnePowerDelay = 0.24f;
    public float CurOnePowerDelay;


    [SerializeField] Collider2D coll;
    [SerializeField] Rigidbody2D rigid;

    [SerializeField] bool RestrictControl;
    [SerializeField] bool Invincible;

    [SerializeField] float LIMIT_WIDTH_L = -5f;
    [SerializeField] float LIMIT_WIDTH_R = 5f;
    [SerializeField] float LIMIT_HEIGHT_T= 6f ;
    [SerializeField] float LIMIT_HEIGHT_B = -6f;
    public bool SetPlayable { set { RestrictControl = value; } }

    private Vector2 inputVector;
    [SerializeField] float PlayerSpeed;

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
    }

    private void Start()
    {
    }

    private void Update()
    {
        CurNormalBulleDelay -= Time.deltaTime;
        CurOnePowerDelay -= Time.deltaTime;

        if (RestrictControl == false) return;


        PlayerMove();
        PlayerNormShoot();
        PlayerSubShoot();
    }


    public void PlayerMove()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(hori, verti);

        float playerSpeed = PlayerSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = PlayerSpeed / 2f;
        }


        if (inputVector != Vector2.zero)
        {
            transform.position += (Vector3)(inputVector * playerSpeed * Time.deltaTime);


            if (transform.position.x < LIMIT_WIDTH_L)
            {
                transform.position = new Vector2(LIMIT_WIDTH_L, transform.position.y);
            }
            else if (transform.position.x > LIMIT_WIDTH_R)
            {
                transform.position = new Vector2(LIMIT_WIDTH_R, transform.position.y);
            }

            if (transform.position.y > LIMIT_HEIGHT_T)
            {
                transform.position = new Vector2(transform.position.x, LIMIT_HEIGHT_T);
            }
            else if (transform.position.y < LIMIT_HEIGHT_B)
            {
                transform.position = new Vector2(transform.position.x, LIMIT_HEIGHT_B);
            }

        }

        


    }

    public void PlayerNormShoot()
    {
        if (CurNormalBulleDelay > 0f) return;

        if (Input.GetKey(KeyCode.Z))
        {
            CurNormalBulleDelay = NormalBulletDelay;
            BulletScriptPlayer bo1 = Instantiate<BulletScriptPlayer>(NormalBullet);
            BulletScriptPlayer bo2 = Instantiate<BulletScriptPlayer>(NormalBullet);
            if (GameManager.Instance.PlayerAttackPower > 2.0f)
            {
                bo1.PowerUp = true;
                bo2.PowerUp = true;
            }
            bo1.transform.position = new Vector3(transform.localPosition.x - 0.1f, transform.position.y, 0f); 
            bo2.transform.position = new Vector3(transform.localPosition.x + 0.1f, transform.position.y, 0f);


        }
    }

    public void PlayerSubShoot()
    {
        if (CurOnePowerDelay > 0f) return;

        if (Input.GetKey(KeyCode.Z) &&  GameManager.Instance.PlayerAttackPower > 1.0f)
        {
            if (powerStack == 2)
            {
                CurOnePowerDelay = 0.5f;
                powerStack = 0;
            }
            else
            {
                CurOnePowerDelay = 0.25f;
                powerStack++;
            }
            CurOnePowerDelay = OnePowerDelay;
            BulletScriptPlayerSub bo1 = Instantiate<BulletScriptPlayerSub>(PowerBullet);
            BulletScriptPlayerSub bo2 = Instantiate<BulletScriptPlayerSub>(PowerBullet);
            if (GameManager.Instance.PlayerAttackPower > 2.0f)
            {
                bo1.PowerUp = true;
                bo2.PowerUp = true;
            }
            bo1.transform.position = new Vector3(transform.localPosition.x - 0.2f, transform.position.y - 0.15f, 0f);
            bo2.transform.position = new Vector3(transform.localPosition.x + 0.2f, transform.position.y - 0.15f, 0f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            if (Invincible) return;
            if (RestrictControl == false) return;

            if (Hit == null)
            {
                Hit = HitEffect();
                StartCoroutine(Hit);
            }

        }
    }

    IEnumerator Hit;

    IEnumerator HitEffect()
    {
        Invincible = true;
        GameManager.Instance.MinusHP();
        for (int i = 0; i < 7; i++)
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.05f);

            spriteRenderer.color = Color.white;
        }

        Invincible = false;
        Hit = null;

    }

    IEnumerator Dead()
    {
        yield return null;
    }

}