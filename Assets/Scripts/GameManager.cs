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
            if (_uiManager != null)
            {
                _uiManager.GameOver();
            }
        }
    }

}
