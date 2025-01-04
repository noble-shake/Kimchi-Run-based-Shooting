using UnityEngine;

public class BulletScriptEnemy: BulletScript
{
    [SerializeField] float FireSpeed;
    float curTime;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        transform.position += new Vector3(0f, FireSpeed * (Mathf.Clamp(curTime, 0.05f, 1f)) * Time.deltaTime, 0f);

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
