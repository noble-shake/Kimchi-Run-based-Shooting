using Unity.VisualScripting;
using UnityEngine;

public class Scrilling : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float Accelator = 1f;
    [SerializeField] float CurTime;

    public void SetAccelator(float _value)
    { 
        Accelator = _value;
    }
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CurTime = Time.fixedDeltaTime;

        spriteRenderer.material.mainTextureOffset += new Vector2(CurTime * Accelator, 0f);
    }
}
