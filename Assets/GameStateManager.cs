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
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject settingsPopup;
    public static GameStateManager Instance { get; private set; }



    private GameStates state;
    private bool paused ;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetNextTurn()
    {
        switch (state)
        {

            case GameStates.Player1Turn:
                //if not wincon
                state = GameStates.Player2Turn;
                break;
            //else
            //state= Game.States.Player1Wins;

            case GameStates.Player2Turn:
                //if not wincon
                state = GameStates.Player1Turn;
                break;
            //else
            //state= Game.States.Player2Wins;

        }
    }

    public GameStates GetCurrentTurn()
    {
        return state;
    }

    public void updateScore(int row, int col)
    {
        if (paused) return;
        
        if (state == GameStates.Player1Turn)
        {
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

                popup.SetActive(true);
                state = GameStates.Player1Win;
            }

        }
        else if (state == GameStates.Player2Turn)
        {
            player_2_win_con[row]++;
            player_2_win_con[col + 3]++;
            if (row == col)
                player_2_win_con[6]++;
            if (2 - row == col)
                player_2_win_con[7]++;

            if (player_2_win_con[row] == 3 || player_2_win_con[col + 3] == 3 || player_2_win_con[6] == 3 ||
                player_2_win_con[7] == 3)
            {
                popup.SetActive(true);
                float time = GetComponentInParent<Timer>().GetTime();
                float minutes = Mathf.FloorToInt(time / 60);
                float seconds = Mathf.FloorToInt(time % 60);
                finalTimeText.text = string.Format("The round laster {0,0:0} minutes and {1:0:0} seconds", minutes,
                    seconds);
                state = GameStates.Player2Win;
            }
        }
    }

    public void RestartGame()
    {
        player_1_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        player_2_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        state = GameStates.Player1Turn;

        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tile in tiles)
        {
            var sprite = tile.GetComponent<SpriteRenderer>().sprite = null;
        }

        GetComponentInParent<Timer>().RestartTimer();
    }

    public void ShowSettings()
    {
        paused = true;
        settingsPopup.SetActive(true);
        Instance.GetComponentInParent<Timer>().PauseTimer();
    }

    public void HideSettings()
    {
        paused = false;
        settingsPopup.SetActive(false);
        Instance.GetComponentInParent<Timer>().StartTimer();
    }

}
