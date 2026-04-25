using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupForMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPopup;

    [SerializeField] private GameObject themePicker;
    
    public void ShowThemePicker()
    {
        themePicker.SetActive(true);
        return;
    }
    public void HideThemePicker()
    {
        if (themePicker != null && settingsPopup.activeSelf)
            themePicker.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void ShowSettings()
    {
        if (settingsPopup != null)
            settingsPopup.SetActive(true);
        GameStateManager.Instance.Pause();
        
    }
    
    public void HideSettings()
    {
        if (settingsPopup != null)
            settingsPopup.SetActive(false);

        GameStateManager.Instance.Unpause();
        
    }
    
    public void ExitGame()
    {
       Debug.Log("Exit the game");
    }
}
