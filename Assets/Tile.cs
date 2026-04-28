using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteX;
    [SerializeField] Sprite spriteO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DataHandler.Instance == null) return;
        
        spriteX = DataHandler.Instance.themeIndexX;
        spriteO = DataHandler.Instance.themeIndexO;
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        if (spriteRenderer.sprite != null) return;
        
        var state = GameStateManager.Instance.GetCurrentTurn();
    
        if (state== GameStates.Player1Turn)
        {
            spriteRenderer.sprite = spriteX;
            
        }
        else if(state== GameStates.Player2Turn)
        {
            spriteRenderer.sprite = spriteO;
        }
        if(SoundManager.Instance!=null)
            SoundManager.Instance.PlayPlaceSound();
        
        switch (state) {
            case GameStates.Player1Turn:
                {
                    spriteRenderer.sprite = spriteX;
                    GameStateManager.Instance.UpdateScore(y, x);
                    GameStateManager.Instance.GetNextTurn();
                    break;
                }
            case GameStates.Player2Turn:
                {
                    spriteRenderer.sprite = spriteO;
                    GameStateManager.Instance.UpdateScore(y, x);
                    GameStateManager.Instance.GetNextTurn();
                    break;
                }
        }
        
    }
}
