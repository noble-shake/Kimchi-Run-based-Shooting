using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    AudioSource audioSource;

    [SerializeField] AudioClip IntroSound;
    [SerializeField] AudioClip BackgroundSound;
    [SerializeField] AudioClip EndSound;

    [SerializeField] AudioClip Nicolas;
    [SerializeField] AudioClip DontForgorKimchi;
    [SerializeField] AudioClip Bullet;
    [SerializeField] AudioClip HitSound;

    AudioClip CurClip;
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
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = IntroSound;
        audioSource.Play();
    }

    public void GameStart()
    {
        audioSource.Stop();
        audioSource.clip = BackgroundSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        //if (audioSource.isPlaying == false)
        //{ 
        //    audioSource.Play();
        //}
    }


}
