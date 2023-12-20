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

    void Start()
    {
        _timerActive = true;
        _currentTime = _minutesOnTimer * 60;
    }

    void Update()
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

    public void TimerActive()
    {
        _timerActive = true;
    }
    public void TimerInactive()
    {
        _timerActive = false;
    }

}
