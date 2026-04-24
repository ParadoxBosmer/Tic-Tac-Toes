using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite spriteX;
    [SerializeField] Sprite spriteO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        var state = GameStateManager.Instance.GetCurrentTurn();

        if (state== GameStates.Player1Turn)
        {
            renderer.sprite = spriteX;
            
        }
        else if(state== GameStates.Player2Turn)
        {
            renderer.sprite = spriteO;
        }

        switch (state) {
            case GameStates.Player1Turn:
                {
                    renderer.sprite = spriteX;
                    GameStateManager.Instance.updateScore(x, y);
                    GameStateManager.Instance.GetNextTurn();
                    break;
                }
            case GameStates.Player2Turn:
                {
                    renderer.sprite = spriteO;
                    GameStateManager.Instance.updateScore(x, y);
                    GameStateManager.Instance.GetNextTurn();
                    break;
                }
        }



    }
}
