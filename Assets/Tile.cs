using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite spriteX;
    [SerializeField] Sprite spriteO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer=GetComponentInParent<SpriteRenderer>();
        spriteX = DataHandler.Instance.themeIndexX;
        spriteO = DataHandler.Instance.themeIndexO;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_spriteRenderer.sprite != null) return;
        
        var state = GameStateManager.Instance.GetCurrentTurn();
    
        if (state== GameStates.Player1Turn)
        {
            _spriteRenderer.sprite = spriteX;
            
        }
        else if(state== GameStates.Player2Turn)
        {
            _spriteRenderer.sprite = spriteO;
        }
        if(SoundManager.Instance!=null)
            SoundManager.Instance.PlayPlaceSound();
        
        switch (state) {
            case GameStates.Player1Turn:
                {
                    _spriteRenderer.sprite = spriteX;
                    GameStateManager.Instance.updateScore(y, x);
                    GameStateManager.Instance.GetNextTurn();
                    break;
                }
            case GameStates.Player2Turn:
                {
                    _spriteRenderer.sprite = spriteO;
                    GameStateManager.Instance.updateScore(y, x);
                    GameStateManager.Instance.GetNextTurn();
                    break;
                }
        }
        
    }
}
