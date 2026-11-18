using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] public ItemType itemType;
    [SerializeField] SpriteRenderer spriteRenderer;
    bool CollectionTrigger;
    float curTime;
    bool popCheck;

    public Sprite SetSprite { get { return spriteRenderer.sprite; } set { if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); } spriteRenderer.sprite = value; } }

    private void Start()
    {
        // InitEffect();
        //CollectionTrigger = false;
        //GetComponent<Rigidbody2D>().gravityScale = 0.35f;
    }

    private void OnEnable()
    {

    }

    public void InitEffect(float alignX = 0f, float alignY = 3f)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(alignX, alignY), ForceMode2D.Impulse);
    }

    IEnumerator ItemCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            //if (CollectionTrigger == true) return;
            //CollectionTrigger = true;
            if (ItemCollect == null)
            {
                ItemCollect = ItemCollection();
                StartCoroutine(ItemCollect);
            }
            // StartCoroutine(ItemCollection());
        }
    }

    IEnumerator ItemCollection()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerScript.Instance.transform.position, 10f);

            if (Vector2.Distance(transform.position, PlayerScript.Instance.transform.position) < 0.001f)
            {
                break;
            }

            yield return null;
        }

        if (itemType != ItemType.GoldenItem)
        {
            GameManager.Instance.AddPower(0.1f);
            GameManager.Instance.AddScore(100);
        }
        else
        {
            GameManager.Instance.AddPower(3f);
            GameManager.Instance.AddScore(1000);
        }
        ItemCollect = null;
        gameObject.SetActive(false);

    }
}