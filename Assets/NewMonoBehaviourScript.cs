using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    int[] player_1_win_con = new int[8] {0,0,0,0,0,0,0,0};
    int[] player_2_win_con = new int[8] {0,0,0,0,0,0,0,0};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int row = 3;
        int col = 3;

        GameObject[,] map = new GameObject[row, col];
        //generate
        foreach(GameObject tile in map) {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
