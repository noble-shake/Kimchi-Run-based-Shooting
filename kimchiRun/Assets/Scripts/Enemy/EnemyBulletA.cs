using UnityEngine;

public class EnemyBulletA : MonoBehaviour, IEnemyBullet
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] public float Speed = 5f;

    public void Init()
    {

    }

    public void SetTargetTransform(Transform _trs)
    {

    }

    private void Update()
    {
        transform.position += -transform.up * Time.deltaTime * Speed;

        if(transform.position.x > 10f || transform.position.x < -10f || transform.position.y < -10f) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManger.GetTag(tags.player)))
        { 
            gameObject.SetActive(false);
        }
    }
}