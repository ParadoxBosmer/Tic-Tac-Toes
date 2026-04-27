using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PopupForMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private GameObject endPopup;
    [SerializeField] private GameObject themePicker;
    [SerializeField] private GameObject statisticsPopup;
    [SerializeField] private GameObject exitComfirmation;
    
    public void ShowThemePicker()
    {
        themePicker.SetActive(true);
    }
    public void HideThemePicker()
    {
        if (themePicker != null)
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
        if(GameStateManager.Instance!=null)
            GameStateManager.Instance.Pause();
        
    }
    
    public void HideSettings()
    {
        if (settingsPopup != null)
            settingsPopup.SetActive(false);
        
        if(GameStateManager.Instance!=null)
            GameStateManager.Instance.Unpause();
    }

    public void ShowStatisticsPopup()
    {
        if(statisticsPopup!=null)
            statisticsPopup.SetActive(true);
    }
    public void HideStatisticsPopup()
    {
        if(statisticsPopup!=null)
            statisticsPopup.SetActive(false);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowExitConfirmation()
    {
        if(exitComfirmation!=null)
            exitComfirmation.SetActive(true);
    }
    public void HideExitConfirmation()
    {
        if(exitComfirmation!=null)
            exitComfirmation.SetActive(false);
    }
    

    public void Exit()
    {
        if (endPopup != null)
        {
            SceneManager.LoadScene("Scenes/Main Menu");
        }
    }
}
