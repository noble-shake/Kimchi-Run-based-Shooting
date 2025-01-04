using UnityEngine;

public class Title : MonoBehaviour
{
    public static Title Instance;
    Animator anim;

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
        anim = GetComponent<Animator>();
    }

    public void TriggerAnimation()
    {
        anim.SetTrigger("Start");
    }

    public bool TriggerDoneCheck()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
