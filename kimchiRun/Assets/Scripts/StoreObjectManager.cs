using UnityEngine;

public class StoreObjectManager : MonoBehaviour
{
    public static StoreObjectManager Instance;

    enum BuildingType
    { 
        Left,
        Right,
    }

    [SerializeField] GameObject[] LeftBuildingPrefab;
    [SerializeField] GameObject[] RightBuildingPrefab;
    private int count;
    private float latency = 3f;
    public float SetLatency { set { if (value < 0.3f) { latency = 0.3f; } latency = value; } }

    private float leftDelay;
    private float rightDelay;

    [SerializeField] bool spawnPlay;
    public bool SetSpawn { set { spawnPlay = value; } }

    float Accel = 1f;
    public void SetAccel(float _acc)
    {
        Accel = _acc;
    }

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

    private void FixedUpdate()
    {
        if (spawnPlay == false) return;

        float TimeValue = Time.fixedDeltaTime;
        leftDelay -= TimeValue;
        rightDelay -= TimeValue;

        if (leftDelay < 0f)
        {
            Spawning(BuildingType.Left);
        }

        if (rightDelay < 0f)
        {
            Spawning(BuildingType.Right);
        }

    }

    private void Spawning(BuildingType _type)
    {
        int targetIndex = Random.Range(0, 5);
        switch (_type)
        { 
            case BuildingType.Left:
                leftDelay = Random.Range(0.3f, latency);
                GameObject bo=  Instantiate(LeftBuildingPrefab[targetIndex]);
                bo.GetComponent<building>().SetAccelator = Accel;
                
                break;
            case BuildingType.Right:
                rightDelay = Random.Range(0.3f, latency);
                GameObject bor = Instantiate(RightBuildingPrefab[targetIndex]);
                bor.GetComponent<building>().SetAccelator = Accel;
                break;

        }
    }


}
