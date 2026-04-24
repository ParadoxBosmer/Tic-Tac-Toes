using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public enum GameStates {Player1Turn,Player2Turn,Draw,Player1Win,Player2Win}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    public static GameStateManager Instance { get; private set; }


    private GameStates state;
    int[] player_1_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] player_2_win_con = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

    public void Awake()
    {
        if (Instance!=null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        state = GameStates.Player1Turn;
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

    public void updateScore(int row,int col)
    {
        if (state == GameStates.Player1Turn)
        {
            player_1_win_con[row]++;
            player_1_win_con[col + 3]++;
            if (row == col)
                player_1_win_con[6]++;
            if (2 - row == col)
                player_1_win_con[7]++;

            if (player_1_win_con[row] == 3 || player_1_win_con[col + 3] == 3 || player_1_win_con[6] == 3 || player_1_win_con[7] == 3)
            {
                _textMeshPro.text ="PLayer 1 won";
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

            if (player_2_win_con[row] == 3 || player_2_win_con[col + 3] == 3 || player_2_win_con[6] == 3 || player_2_win_con[7] == 3)
            {
                _textMeshPro.text="PLayer 2 won";
                state = GameStates.Player2Win;
            }
        }
    }
}
