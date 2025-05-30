using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EngamePanel : MonoBehaviour
{
    [SerializeField] private Transform WinPanel;
    [SerializeField] private Transform LosePanel;
    [SerializeField] TextMeshProUGUI scoreText; 
    [SerializeField] TextMeshProUGUI maxCombosText; 
    [SerializeField] TextMeshProUGUI missMatchesText; 

    private ScoreData _scoreData;

    public void OnEndGame(bool win, ScoreData scoreData)
    {
        _scoreData = scoreData;
        gameObject.SetActive(true);
        if(win)
        {
            WinPanel.gameObject.SetActive(true);
            LosePanel.gameObject.SetActive(false);
        }
        else
        {
            WinPanel.gameObject.SetActive(false);
            LosePanel.gameObject.SetActive(true);
        }
        SetScoreText(_scoreData != null ? _scoreData.score.ToString() : "");
        SetMaxCombosText(_scoreData != null ? _scoreData.maxCombos.ToString() : "");
        SetMissMatchedText(_scoreData != null ? _scoreData.missMatches.ToString() : "");
    }

    public void CloseEndgamePanel()
    {
        GameManager.inst.UIManager.ShowMainMenu();
        _scoreData = null;
    }
    public void HidePanel()
    {
        gameObject.SetActive(false);
        WinPanel.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
    }
    private void SetScoreText(string text)
    {
        scoreText.text = text;
    }
    private void SetMaxCombosText(string text)
    {
        maxCombosText.text = text;
    }
    private void SetMissMatchedText(string text)
    {
        missMatchesText.text = text;
    }
}
