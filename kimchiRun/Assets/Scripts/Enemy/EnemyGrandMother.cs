using UnityEngine;

public class EnemyGrandMother : EnemyScript
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


}
