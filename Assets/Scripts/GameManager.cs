using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Present[] _present;
    private UIManager _uiManager;
    [SerializeField] private GameObject _controlsBox;
    private bool _getUIManager;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private AudioSource _audioSource;
    private bool _playLosingSound = true;
    private bool _isGameOver = false;


    private void Start()
    {
        Time.timeScale = 1;
        if (_getUIManager == true)
        {
            _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        }
    }

    private void Update()
    {
        PresentsRemaining();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
        PlayLosingSound();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        _getUIManager = true;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        _getUIManager = false;
        SceneManager.LoadScene(0);
    }

    public void Controls()
    {
        _controlsBox.SetActive(true);
    }
    public void ExitControlsBox()
    {
        _controlsBox.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PresentsRemaining()
    {
        //checks how many presents are in the scene. If zero are remaining, game over text appears
        _present = FindObjectsOfType<Present>();
        int numberOfInstances = _present.Length;

        if (numberOfInstances <= 0)
        {
            _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
            if (_uiManager != null)
            {
                Time.timeScale = 0;
                _uiManager.GameOver();
                _isGameOver = true;
            }
        }
    }

    private void PlayLosingSound()
    {
        if (_isGameOver == true && _playLosingSound == true)
        {
            _audioSource.Play();
            _playLosingSound = false;
        }
    }

}
