using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public enum GameStates {Player1Turn,Player2Turn,Draw,Player1Win,Player2Win}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private GameObject endPopup;
    [SerializeField] private TextMeshProUGUI crossesCount;
    [SerializeField] private TextMeshProUGUI circlesCount;
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
                    state = GameStates.Draw;
                    winnerText.text = "Draw";
                    float time = GetComponentInParent<Timer>().GetTime();
                    float minutes = Mathf.FloorToInt(time / 60);
                    float seconds = Mathf.FloorToInt(time % 60);
                    finalTimeText.text =
                        string.Format("The round laster {0:00} minutes and {1:00} seconds", minutes, seconds);

                    endPopup.SetActive(true);
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

                if (player_1_win_con[row] == 3 || player_1_win_con[col + 3] == 3 || player_1_win_con[6] == 3 ||
                    player_1_win_con[7] == 3)
                {

                    winnerText.text = "PLayer 1 won";
                    float time = GetComponentInParent<Timer>().GetTime();
                    float minutes = Mathf.FloorToInt(time / 60);
                    float seconds = Mathf.FloorToInt(time % 60);
                    finalTimeText.text =
                        string.Format("The round laster {0:00} minutes and {1:00} seconds", minutes, seconds);

                    endPopup.SetActive(true);
                    state = GameStates.Player1Win;
                    current_turns++;
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

                if (player_2_win_con[row] == 3 || player_2_win_con[col + 3] == 3 || player_2_win_con[6] == 3 ||
                    player_2_win_con[7] == 3)
                {
                    endPopup.SetActive(true);
                    float time = GetComponentInParent<Timer>().GetTime();
                    float minutes = Mathf.FloorToInt(time / 60);
                    float seconds = Mathf.FloorToInt(time % 60);
                    finalTimeText.text = string.Format("The round laster {0:00} minutes and {1:00} seconds", minutes,
                        seconds);
                    state = GameStates.Player2Win;
                    current_turns++;
                }

                break;
            }
        }
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

        GetComponentInParent<Timer>().RestartTimer();
    }
    

}
