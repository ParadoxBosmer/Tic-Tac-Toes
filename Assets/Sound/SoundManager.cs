using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip --------")]

    [SerializeField] AudioClip background;
    [SerializeField] public AudioClip placeSign;
    [SerializeField] AudioClip winSound;

    [Header("------- Ticks --------")] 
    public bool mutedMusic=false;
    public bool mutedSFX=false;
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
        musicSource.loop = true; 
        
        if (mutedMusic) MuteMusic();
        if (mutedSFX) MuteSFX();
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
    
    public void MuteMusic()
    { 
        mutedMusic = true;
        musicSource.volume = 0;
    }

    public void UnMuteMusic()
    {
        mutedMusic = false;
        musicSource.volume = 50;

    }
    
    public void MuteSFX()
    { 
        mutedSFX = true;
        SFXSource.volume = 0;
    }

    public void UnMuteSFX()
    {
        mutedSFX = false;
        SFXSource.volume = 50;

    }
    
}
