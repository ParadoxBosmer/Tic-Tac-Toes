using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _elapsedTime;

    public bool playing = true;

    // Update is called once per frame
    void Update()
    {
        if(!playing) return;
        
        _elapsedTime += Time.deltaTime;
        var minutes = Mathf.FloorToInt(_elapsedTime / 60);
        var seconds=Mathf.FloorToInt(_elapsedTime % 60);
        timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartTimer()
    {
        _elapsedTime = 0;
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
        return _elapsedTime;
    }

}
