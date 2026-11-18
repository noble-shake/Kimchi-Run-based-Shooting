using UnityEngine;
using UnityEngine.UI;

public class Blank : MonoBehaviour
{
    CanvasGroup cvs;
    public float timing;

    private void Start()
    {
        cvs = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        cvs.alpha += Time.deltaTime * timing;
        if (cvs.alpha >= 1f)
        {
            cvs.alpha = 0f;
        }
    }
}
