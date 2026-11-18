using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] PlayerScript playerPrefab;

    [SerializeField] Button StartButton;
    [SerializeField] CanvasGroup TutorialScreen;

    [SerializeField] bool GamePlay;
    [SerializeField] float ElapsedTime;
    public float CurrentTime { get { return ElapsedTime; } }

    [SerializeField] int Score = 0;
    [SerializeField] int PlayerHP = 2;
    [SerializeField] public float PlayerAttackPower = 0.0f;
    [SerializeField] TMP_Text PowerText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text TimeText;

    [SerializeField] Sprite EmptyHeart;
    [SerializeField] Sprite Heart;
    [SerializeField] List<Image> Hearts;

    [SerializeField] public TMP_Text BossHPText;
    [SerializeField] public Image BossHPBar;
    [SerializeField] public TMP_Text BossTimer;

    [SerializeField] public TMP_Text BadResultScore;
    [SerializeField] public TMP_Text ResultScore;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameEndMenu;
    [SerializeField] Button End1;
    [SerializeField] Button End2;

    public void OnClickedEnd()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    int HPCount = 2;
    public int HP { get { return PlayerHP; }}
    public float Power { get { return PlayerAttackPower; }}

    public void AddScore(int _score)
    {
        Score += _score;
        ScoreText.text = Score.ToString();
    }

    public void AddHP()
    {
        PlayerHP++;
        if (PlayerHP > 2) PlayerHP = 2;
        for (int i = 0; i < PlayerHP; i++)
        {
            Hearts[i].sprite = Heart;
        }
    }

    public IEnumerator BadEnding()
    {
        yield return new WaitForSecondsRealtime(2f);

        GameOverMenu.SetActive(true);
        BadResultScore.text = Score.ToString();

    }

    public void DeadEnd()
    {
        StartCoroutine(GoodEnding());
    }

    public IEnumerator GoodEnding()
    {
        yield return new WaitForSecondsRealtime(5f);

        GameEndMenu.SetActive(true);
        ResultScore.text = Score.ToString();

    }

    public void MinusHP()
    {
        if (PlayerHP < 0)
        {
            Time.timeScale = 0f;
            StartCoroutine(BadEnding());
            return;
        }
        Hearts[PlayerHP].sprite = EmptyHeart;
        PlayerHP--;





    }

    public void AddPower(float _power)
    {
        PlayerAttackPower += _power;
        if (PlayerAttackPower > 3.0f) PlayerAttackPower = 3.0f;
        PowerText.text = PlayerAttackPower.ToString();
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

        Screen.SetResolution(1280, 960, false);

    }

    private void Start()
    {
        End1.onClick.AddListener(OnClickedEnd);
        End2.onClick.AddListener(OnClickedEnd);
        GamePlay = false;
        StartButton.onClick.AddListener(ButtonStart);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if(GamePlay) ElapsedTime += Time.fixedDeltaTime;
        TimeText.text = ElapsedTime.ToString();
    }


    #region Button START
    public void ButtonStart()
    {
        StartCoroutine(StartEvent());
    }

    IEnumerator StartEvent()
    {
        PlayerScript po = Instantiate(playerPrefab);

        Title.Instance.TriggerAnimation();

        TutorialScreen.gameObject.SetActive(false);

        while (true)
        { 
            yield return null;
            if (Title.Instance.TriggerDoneCheck())
            {
                break;
            }
        }

        yield return new WaitForSeconds(1f);

        GamePlay = true;
        StoreObjectManager.Instance.SetSpawn = true;
        Playing();
        AudioManager.Instance.GameStart();
        po.SetPlayable = true;
        
        // Player Instantiate;
    }
    #endregion

    public IEnumerator Phase1Wave1()
    {
        StoreObjectManager.Instance.SetAccel(4f);
        for (int i = 0; i < 5; i++)
        {
            EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.LeftDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.2f));

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 5; i++)
        {

            EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.RightDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.8f));

            yield return new WaitForSeconds(0.5f);
        }

        Sequences.Instance.QueTrigger = false;

    }

    public IEnumerator Phase1Wave2()
    {

        for (int i = 0; i < 5; i++)
        {
            EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.LeftDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.4f), 6);
            yield return new WaitForSeconds(0.25f);
            EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.RightDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.6f), 6);
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);

        Sequences.Instance.QueTrigger = false;

    }

    public IEnumerator Phase2Wave1()
    {
        StoreObjectManager.Instance.SetAccel(8f);
        StoreObjectManager.Instance.SetLatency = 1.5f;

        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.2f), 35);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.4f), 35);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.6f), 35);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDown, AttackCommandGroup.Trace, EnemyManager.Instance.SpawnAligner(0.8f), 35);
        yield return null;

        Sequences.Instance.QueTrigger = false;
    }

    public IEnumerator Phase2Wave2()
    {


        EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.NormalDown, AttackCommandGroup.ThreeWay, EnemyManager.Instance.SpawnAligner(0.2f), 40);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.NormalDown, AttackCommandGroup.ThreeWay, EnemyManager.Instance.SpawnAligner(0.8f), 40);
        yield return null;

        Sequences.Instance.QueTrigger = false;
    }

    public IEnumerator Phase2Wave3()
    {


        EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.NormalDown, AttackCommandGroup.ThreeWay, EnemyManager.Instance.SpawnAligner(0.44f), 40);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.NormalDown, AttackCommandGroup.ThreeWay, EnemyManager.Instance.SpawnAligner(0.56f), 20);
        yield return null;

        Sequences.Instance.QueTrigger = false;
    }

    public IEnumerator Phase2Wave4()
    {


        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDown, AttackCommandGroup.TripleThreeWay, EnemyManager.Instance.SpawnAligner(0.4f), 35);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDown, AttackCommandGroup.TripleThreeWay, EnemyManager.Instance.SpawnAligner(0.6f), 35);

        yield return null;

        Sequences.Instance.QueTrigger = false;
    }

    public IEnumerator Phase3Wave1()
    {


        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDownOneShot, AttackCommandGroup.CircleSpread, EnemyManager.Instance.SpawnAligner(0.5f), 120);

        yield return new WaitForSeconds(10f);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.GrandMother, MoveCommandGroup.NormalDownOneShot, AttackCommandGroup.CircleSpread, EnemyManager.Instance.SpawnAligner(0.2f), 130);

        yield return new WaitForSeconds(10f);
        EnemyManager.Instance.EnemySpawn(EnemyGroup.DalBae, MoveCommandGroup.NormalDownOneShot, AttackCommandGroup.CircleSpread, EnemyManager.Instance.SpawnAligner(0.6f), 140);

        yield return null;
        Sequences.Instance.QueTrigger = false;
    }

    public IEnumerator Phase4Boss()
    {
        EnemyManager.Instance.BossSpawn();
        yield return null;
    }

    // public void GenerateSequence(List<IEnumerator> methods, List<float> delays)
    public void Playing()
    {
        List<IEnumerator> methods = new List<IEnumerator>();
        List<float> timing = new List<float>();

        methods.Add(Phase1Wave1());
        timing.Add(8f);

        methods.Add(Phase1Wave2());
        timing.Add(2f);

        methods.Add(Phase2Wave1());
        timing.Add(6f);

        methods.Add(Phase2Wave2());
        timing.Add(12f);

        methods.Add(Phase2Wave3());
        timing.Add(6f);

        methods.Add(Phase2Wave4());
        timing.Add(8f);

        methods.Add(Phase3Wave1());
        timing.Add(12f);

        methods.Add(Phase4Boss());
        timing.Add(12f);

        Sequences.Instance.GenerateSequence(methods, timing);
    }

}

