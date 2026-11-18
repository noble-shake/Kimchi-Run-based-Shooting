using Unity.VisualScripting;
using UnityEngine;

public class ScrillingMesh : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float Accelator = 1f;
    [SerializeField] float CurTime;

    public void SetAccelator(float _value)
    {
        Accelator = _value;
    }


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CurTime = Time.fixedDeltaTime;

        meshRenderer.material.mainTextureOffset += new Vector2(CurTime * Accelator, 0f);
    }
}
