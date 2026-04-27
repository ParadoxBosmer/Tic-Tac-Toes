using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupForMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private GameObject endPopup;

    [SerializeField] private GameObject themePicker;
    [SerializeField] private GameObject statisticsPopup;
    
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
       Debug.Log("Exit the game");
    }

    public void Exit()
    {
        if (endPopup != null)
        {
            SceneManager.LoadScene("Scenes/Main Menu");
        }
    }
}
