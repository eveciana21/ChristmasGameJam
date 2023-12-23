using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private float _minutesOnTimer;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private Slider _anvilSlider;
    [SerializeField] private GameObject _useAnvilText;
    [SerializeField] private GameObject _victoryMenu;

    void Start()
    {
        _timerActive = true;
        _currentTime = _minutesOnTimer * 60;
        _gameOverText.SetActive(false); // sets game over text false by default
        _anvilSlider.value = 0;
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
                Time.timeScale = 0;
                _victoryMenu.SetActive(true);
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

    public void AnvilSlider(float boostPercent)
    {
        _anvilSlider.value = boostPercent;

        _anvilSlider.maxValue = 100f;
        _anvilSlider.minValue = 0f;

        if (_anvilSlider.value == 100)
        {
            _useAnvilText.SetActive(true);
        }
        else
        {
            _useAnvilText.SetActive(false);
        }

    }

}
