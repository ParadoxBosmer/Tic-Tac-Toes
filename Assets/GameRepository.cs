using System;
using UnityEngine;

public class GameRepository : MonoBehaviour
{
    public int numberOfGames;
    public int player1WinCount;
    public int player2WinCount;
    public int drawCount;
    public float averageDurationSeconds;
    public int averageDurationTurns;
    
    public void AddGame(float durationSeconds, string winner, int numberOfTurns,string filepath)
    {
        try
        {
            using System.IO.StreamWriter file= new System.IO.StreamWriter(@filepath, true);
            file.WriteLine(numberOfTurns+ "," +winner+","+durationSeconds);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong during writing",ex);
        }
    }

    public void CheckStatistics(string filepath)
    {
        float duration = 0;
        float turns = 0;

        try
        {
            string[] lines = System.IO.File.ReadAllLines(@filepath);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                numberOfGames++;
                switch (fields[1])
                {
                    case "Player1Win":
                    {
                        player1WinCount++;
                        break;
                    }
                    case "Player2Win":
                    {
                        player2WinCount++;
                        break;
                    }
                    case "Draw":
                    {
                        drawCount++;
                        break;
                    }
                }
                duration+=float.Parse(fields[2]);
                turns += int.Parse(fields[0]);
            }

            averageDurationTurns = (int)Math.Ceiling(turns/numberOfGames)  ;
            averageDurationSeconds = (float) Math.Round(duration / numberOfGames , 2);
        }
        catch (Exception ex)
        {
            
            throw new ApplicationException("Something went wrong during reading",ex);
        }
    }
}
