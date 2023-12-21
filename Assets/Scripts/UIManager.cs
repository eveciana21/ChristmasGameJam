using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private float _minutesOnTimer;
    [SerializeField] private TMP_Text _timeText;

    [SerializeField] private GameObject _gameOverText;

    void Start()
    {
        _timerActive = true;
        _currentTime = _minutesOnTimer * 60;
        _gameOverText.SetActive(false); // sets game over text false by default
    }

    void Update()
    {
        Timer();
    }

    public void TimerActive()
    {
        _timerActive = true;
    }
    public void TimerInactive()
    {
        _timerActive = false;
    }

    private void Timer()
    {
        if (_timerActive)
        {
            _currentTime = _currentTime - Time.deltaTime;
            if (_currentTime <= 0)
            {
                _timerActive = false;
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _timeText.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
    }

    public void GameOver()
    {
        //Enables game over text and button & pauses game speed
        Time.timeScale = 0;
        _gameOverText.SetActive(true);
    }



}
