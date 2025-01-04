using UnityEngine;


public class building : MonoBehaviour
{
    [SerializeField] float Accelator;
    [SerializeField] float CurTime;
    
    [SerializeField] Vector2 OriginVector;
    [SerializeField] Vector2 DestinationVector;

    public float SetAccelator { set { Accelator = value; } }

    private void Start()
    {
        transform.position = OriginVector;
    }

    private void Update()
    {
        CurTime += Time.deltaTime * 0.5f * Accelator;
        if (CurTime > 1f) CurTime = 1f;
        transform.position = Vector2.Lerp(OriginVector, DestinationVector, CurTime);


        if (Vector2.Distance(DestinationVector, transform.position) < 0.0001f)
        { 
            Destroy(gameObject);
        }
    }


}