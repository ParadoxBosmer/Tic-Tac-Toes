using System.Collections;
using System.IO;
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
    [SerializeField] private GameRepository repo;
    
    [SerializeField] private Transform[] lineStartPoints;
    [SerializeField] private Transform[] lineEndPoints;
    public static GameStateManager Instance { get; private set; }

    private GameStates _state;
    public bool paused;
    private int _maxTurns = 9;
    private int _currentTurns;
    private int _circles;
    private string savePath;
    private int _crosses;
    int[] _player1WinCon = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] _player2WinCon = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        paused = false;
        savePath=DataHandler.Instance.savePath;
    }

    void Start()
    {
        _state = GameStates.Player1Turn;
        _currentTurns = 0;
        _circles = 4;
        _crosses = 5;
        crossesCount.text = _crosses.ToString();
        circlesCount.text = _circles.ToString();
    }

    public void UpdateText()
    {
        switch (_state)
        {
            case GameStates.Player1Turn:
            {
                _crosses--;
                crossesCount.text = _crosses.ToString();
                break;
            }
            case GameStates.Player2Turn:
            {
                _circles--;
                circlesCount.text = _circles.ToString();
                break;
            }
        }
    }
    public void GetNextTurn()
    {
        switch (_state)
        {
            case GameStates.Player1Turn:
            {
                _state = GameStates.Player2Turn;
                _currentTurns++;
                if (_currentTurns == _maxTurns)
                {
                    DrawSequence();
                }

                break;
        }
            case GameStates.Player2Turn:
            {
                _state = GameStates.Player1Turn;
                _currentTurns++;
                break;
            }
        }
        
    }

    private void DrawSequence()
    {
        _state = GameStates.Draw;
        winnerText.text = "Draw";
        float time = GetComponentInParent<Timer>().GetTime();
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        finalTimeText.text =
            string.Format("The round laster {0:00} minutes and {1:00} seconds", minutes, seconds);
        endPopup.SetActive(true);
        repo.AddGame(time, _state.ToString(), _currentTurns, savePath);
    }

    public GameStates GetCurrentTurn()
    {
        return _state;
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

    public void UpdateScore(int row, int col)
    {
        switch (_state)
        {
            case GameStates.Player1Turn:
            {
                UpdateText();
                _player1WinCon[row]++;
                _player1WinCon[col + 3]++;
                if (row == col)
                    _player1WinCon[6]++;
                if (2 - row == col)
                    _player1WinCon[7]++;

                int winningIndex = GetWinningIndex(_player1WinCon, row, col);
                
                if (winningIndex != -1)
                {
                    _state = GameStates.Player1Win;
                    _currentTurns++;
                    StartCoroutine(WinSequence("Player 1 won", winningIndex));
                }

                break;
            }
            case GameStates.Player2Turn:
            {
                UpdateText();
                _player2WinCon[row]++;
                _player2WinCon[col + 3]++;
                if (row == col)
                    _player2WinCon[6]++;
                if (2 - row == col)
                    _player2WinCon[7]++;

                int winningIndex = GetWinningIndex(_player2WinCon, row, col);
                
                if (winningIndex != -1)
                {
                    _state = GameStates.Player2Win;
                    _currentTurns++;
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
        repo.AddGame(time, _state.ToString(), _currentTurns, DataHandler.Instance.savePath);
    }

    public void RestartGame()
    {
        _player1WinCon = new[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        _player2WinCon = new[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        _state = GameStates.Player1Turn;
        _currentTurns = 0;

        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tile in tiles)
        {
            tile.GetComponent<SpriteRenderer>().sprite = null;
        }

        _crosses = 5;
        _circles = 4;
        crossesCount.text = _crosses.ToString();
        circlesCount.text = _circles.ToString();
        lineDrawer.RestartLine();
        
        GetComponentInParent<Timer>().RestartTimer();
        endPopup.SetActive(false);
    }
    

}
