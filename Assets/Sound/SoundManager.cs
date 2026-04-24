using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip --------")]

    [SerializeField] AudioClip background;
    [SerializeField] AudioClip placeSign;
    [SerializeField] AudioClip winSound;
    
    public static SoundManager Instance { get; private set; }

    public void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void Start()
    {
        musicSource.clip= background;
        musicSource.Play();
    }
    
    public void PlayWinSound()
    {
        musicSource.clip = winSound;
        musicSource.Play();
    }
    public void PlayPlaceSound()
    {
        musicSource.clip = placeSign;
        musicSource.Play();
    }
}
