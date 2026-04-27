using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteX;
    [SerializeField] Sprite spriteO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer=GetComponentInParent<SpriteRenderer>();
        spriteX = DataHandler.Instance.themeIndexX;
        spriteO = DataHandler.Instance.themeIndexO;
    }
    
    private void OnMouseDown()
    {
        if (spriteRenderer.sprite != null || GameStateManager.Instance.paused) return;
        
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
