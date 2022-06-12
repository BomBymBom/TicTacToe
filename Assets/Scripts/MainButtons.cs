using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtons : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _winLosePanel;
    [SerializeField] GameController _gameController;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToStartMenu()
    {
        _settingsPanel.SetActive(false);
        //_gamePanel.SetActive(false);
        _winLosePanel.SetActive(false);
        _gameController.DeactivateWinLosePanel();
        _startPanel.SetActive(true);
    }


    public void GoToGamePanel()
    {
        _settingsPanel.SetActive(false);
        _startPanel.SetActive(false);
        //_gamePanel.SetActive(true);
    }

    public void RestartTheGame()
    {
        _winLosePanel.SetActive(false);
        _gameController.DeactivateWinLosePanel();
        _gameController.ResetCells();
    }
}
