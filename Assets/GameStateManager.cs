using System.Collections;
using TMPro;
using UnityEngine;

public enum GameStates {Player1Turn,Player2Turn,Draw,Player1Win,Player2Win}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private GameObject endPopup;
    [SerializeField] private TextMeshProUGUI crossesCount;
    [SerializeField] private TextMeshProUGUI circlesCount;
    [SerializeField] private LineDrawer lineDrawer;
    
    [SerializeField] private Transform[] lineStartPoints;
    [SerializeField] private Transform[] lineEndPoints;
    public static GameStateManager Instance { get; private set; }

    private GameStates state;
    private bool paused ;
    private int max_turns = 9;
    private int current_turns;
    private int circles;
    private int crosses;
    int[] player_1_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] player_2_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        state = GameStates.Player1Turn;
        paused = false;
        current_turns = 0;
        circles = 4;
        crosses = 5;
        crossesCount.text = crosses.ToString();
        circlesCount.text = circles.ToString();
    }

    public void UpdateText()
    {
        switch (state)
        {
            case GameStates.Player1Turn:
            {
                crosses--;
                crossesCount.text = crosses.ToString();
                break;
            }
            case GameStates.Player2Turn:
            {
                circles--;
                circlesCount.text = circles.ToString();
                break;
            }
        }
    }
    public void GetNextTurn()
    {
        switch (state)
        {
            case GameStates.Player1Turn:
            {
                state = GameStates.Player2Turn;
                current_turns++;
                if (current_turns == max_turns)
                {
                    DrawSequence();
                }

                break;
        }
            case GameStates.Player2Turn:
            {
                state = GameStates.Player1Turn;
                current_turns++;
                break;
            }
        }
        
    }

    private void DrawSequence()
    {
        state = GameStates.Draw;
        winnerText.text = "Draw";
        float time = GetComponentInParent<Timer>().GetTime();
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        finalTimeText.text =
            string.Format("The round laster {0:00} minutes and {1:00} seconds", minutes, seconds);

        endPopup.SetActive(true);
    }

    public GameStates GetCurrentTurn()
    {
        return state;
    }

    public void Pause()
    {
        paused = true;
        GetComponentInParent<Timer>().playing = false;
    }

    public void Unpause()
    {
        paused = false;
        GetComponentInParent<Timer>().playing = true;
    }

    public void updateScore(int row, int col)
    {
        if (paused) return;

        switch (state)
        {
            case GameStates.Player1Turn:
            {
                UpdateText();
                player_1_win_con[row]++;
                player_1_win_con[col + 3]++;
                if (row == col)
                    player_1_win_con[6]++;
                if (2 - row == col)
                    player_1_win_con[7]++;

                int winningIndex = GetWinningIndex(player_1_win_con, row, col);
                
                if (winningIndex != -1)
                {
                    state = GameStates.Player1Win;
                    current_turns++;
                    StartCoroutine(WinSequence("Player 1 won", winningIndex));
                }

                break;
            }
            case GameStates.Player2Turn:
            {
                UpdateText();
                player_2_win_con[row]++;
                player_2_win_con[col + 3]++;
                if (row == col)
                    player_2_win_con[6]++;
                if (2 - row == col)
                    player_2_win_con[7]++;

                int winningIndex = GetWinningIndex(player_2_win_con, row, col);
                
                if (winningIndex != -1)
                {
                    state = GameStates.Player1Win;
                    current_turns++;
                    StartCoroutine(WinSequence("Player 2 won", winningIndex));
                }
                
                break;
            }
        }
    }
    
    private int GetWinningIndex(int[] winConditions, int row, int col)
    {
        if (winConditions[col + 3] == 3) return col + 3;
        if (winConditions[row] == 3) return row;
        if (winConditions[6] == 3) return 6;
        if (winConditions[7] == 3) return 7;
        
        return -1; // No win yet
    }
    private IEnumerator WinSequence(string winnerString, int winIndex)
    {
        Vector3 startPoint = lineStartPoints[winIndex].position;
        Vector3 endPoint = lineEndPoints[winIndex].position;
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayWinSound();
        yield return StartCoroutine(lineDrawer.AnimateLine(startPoint, endPoint));

        winnerText.text = winnerString;
        float time = GetComponentInParent<Timer>().GetTime();
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        
        finalTimeText.text = string.Format("The round lasted {0:00} minutes and {1:00} seconds", minutes, seconds);
        
        endPopup.SetActive(true);
    }

    public void RestartGame()
    {
        player_1_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        player_2_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        state = GameStates.Player1Turn;
        current_turns = 0;

        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tile in tiles)
        {
            var sprite = tile.GetComponent<SpriteRenderer>().sprite = null;
        }

        crosses = 5;
        circles = 4;
        crossesCount.text = crosses.ToString();
        circlesCount.text = circles.ToString();
        lineDrawer.RestartLine();
        
        GetComponentInParent<Timer>().RestartTimer();
    }
    

}
