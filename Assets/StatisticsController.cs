using System;
using TMPro;
using UnityEngine;

public class StatisticsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1WinsText;
    [SerializeField] private TextMeshProUGUI player2WinsText;
    [SerializeField] private TextMeshProUGUI drawsText;
    [SerializeField] private TextMeshProUGUI averageDurationSecondsText;
    [SerializeField] private TextMeshProUGUI averageDurationTurnsText;

    [SerializeField] private GameRepository repo;
    [SerializeField] private String path;
    

    public void Start()
    {
        repo.CheckStatistics(path);
        player1WinsText.text=repo.player1WinCount.ToString();
        player2WinsText.text= repo.player2WinCount.ToString();
        drawsText.text = repo.drawCount.ToString();
        averageDurationSecondsText.text=repo.averageDurationSeconds.ToString();
        averageDurationTurnsText.text=repo.averageDurationTurns.ToString();
        
    }


}
