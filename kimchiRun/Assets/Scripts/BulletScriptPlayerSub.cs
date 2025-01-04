using UnityEngine;

public class BulletScriptPlayerSub : BulletScript
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float FireSpeed;
    float curTime;
    public bool PowerUp;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (PowerUp == false)
        {
            spriteRenderer.color = Color.white;
            transform.position += new Vector3(0f, FireSpeed * 2f * Time.deltaTime, 0f);
        }
        else
        {
            spriteRenderer.color = Color.yellow;
            transform.position += new Vector3(0f, FireSpeed * 2.25f * Time.deltaTime, 0f);
        }


        if (transform.position.y > 10f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManger.GetTag(tags.Enemy)))
        {

            Destroy(gameObject);
        }
    }

}
