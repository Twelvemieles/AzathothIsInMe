using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private EngamePanel engamePanel;
    [SerializeField] private MainMenuPanel mainMenuPanel;
    [SerializeField] private Transform gameplayPanel;
    public void OnEndgame(bool win, ScoreData scoreData)
    {
        engamePanel.OnEndGame(win, scoreData);
    }    

    public void ShowMainMenu()
    {
        mainMenuPanel.gameObject.SetActive(true);
        HideGamePlayScreen();
    }
    public void HideMainMenu()
    {
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void ShowGameplayScreen( )
    {
        HideMainMenu();
        gameplayPanel.gameObject.SetActive(true);
    }
    public void HideGamePlayScreen()
    {

        engamePanel.HidePanel();
    }
}
