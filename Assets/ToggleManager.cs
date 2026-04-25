using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    [SerializeField] public Toggle musicToggle;
    [SerializeField] public Toggle sfxToggle;

    void Start()
    {
        musicToggle.SetIsOnWithoutNotify(!SoundManager.Instance.mutedMusic);
        sfxToggle.SetIsOnWithoutNotify(!SoundManager.Instance.mutedSFX);

        musicToggle.onValueChanged.AddListener(ToggleMusic);
        sfxToggle.onValueChanged.AddListener(ToggleSFX);
    }

    private void ToggleMusic(bool isToggleOn)
    {
        if (isToggleOn)
            SoundManager.Instance.UnMuteMusic();
        else
            SoundManager.Instance.MuteMusic();
    }

    private void ToggleSFX(bool isToggleOn)
    {
        if (isToggleOn)
            SoundManager.Instance.UnMuteSFX();
        else
            SoundManager.Instance.MuteSFX();
    }
    
}
