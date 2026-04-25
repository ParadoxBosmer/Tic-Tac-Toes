using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    public void RestartGame()
    {
        GameStateManager.Instance.RestartGame();
        parent.SetActive(false);
    }

    public void ExitGame()
    {
        parent.SetActive(false);
    }
    
}
