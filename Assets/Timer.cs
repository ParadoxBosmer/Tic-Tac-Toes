using System;
using UnityEngine;
using TMPro;
using Unity.Mathematics.Geometry;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime;

    public bool playing = true;

    // Update is called once per frame
    void Update()
    {
        if(!playing) return;
        
        elapsedTime += Time.deltaTime;
        var minutes = Mathf.FloorToInt(elapsedTime / 60);
        var seconds=Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartTimer()
    {
        elapsedTime = 0;
        playing = true;
    }

    public void PauseTimer()
    {
        playing = false;
    }

    public void StartTimer()
    {
        playing = true;
    }

    public float GetTime()
    {
        playing = false;
        return elapsedTime;
    }

}
