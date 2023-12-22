using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Present[] _present;
    private UIManager _uiManager;

    private void Start()
    {
        Time.timeScale = 1;
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    private void Update()
    {
        PresentsRemaining();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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
            _uiManager.GameOver();
        }
    }

}
