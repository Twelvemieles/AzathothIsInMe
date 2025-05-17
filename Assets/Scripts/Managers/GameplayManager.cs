using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    public GameplayDataScriptableObject GameDataConfig;
    public event Action<bool, ScoreData> OnEndGameAction;

    [SerializeField] private DealerController dealerController;
    [SerializeField] private MatchController matchController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private TimerController timerController;


    public void Start()
    {
        dealerController.OnCardClicked += OnCardClicked;

        matchController.OnCardsMatch += OnCardsMatch;
        matchController.OnCardsMissMatch += OnCardsMissMatch;

        timerController.OnTimeEnds += OnTimeEnds;


    }


    public void StartGamePlay(int horizontalCards, int verticalCards)
    {
        dealerController.PopulateBoard(horizontalCards, verticalCards);
        timerController.Init(GameDataConfig.gameplayData.gameTimeSeconds);
        scoreController.ShowScorePanel();
        scoreController.ClearData();
    }


    private void OnCardClicked(CardController card)
    {
        matchController.CheckCard(card);
        GameManager.inst.AudioManager.PlaySFX("FlipCard");
    }
    private void OnCardsMatch(CardController cardA, CardController cardB)
    {
        scoreController.AddScore(GameDataConfig.gameplayData.matchPoints);
        scoreController.AddCombo();
        dealerController.DeleteCard(cardA);
        dealerController.DeleteCard(cardB);

        GameManager.inst.AudioManager.PlaySFX("Match");
        CheckIfWin();

    }
 
    private void OnCardsMissMatch()
    {
        scoreController.ResetCombos();
        GameManager.inst.AudioManager.PlaySFX("MissMatch");
    }
    private void CheckIfWin()
    {
        if (IsCardTableEmpty())
        {
            OnEndGame(true);
        }
    }

    private bool IsCardTableEmpty()
    {
        return dealerController.Cards.Count <= 0;
    }

    private void OnTimeEnds()
    {
        OnEndGame(false);
    }
    public void OnEndGame(bool win)
    {
        scoreController.SetScoreTime(timerController.ActualTime);

        OnEndGameAction?.Invoke(win,scoreController.ScoreData);

        scoreController.ClearData();
        scoreController.HideScorePanel();

        matchController.ClearCheck();

        timerController.OnStopTimer();


        if(win)
        {
            GameManager.inst.AudioManager.PlaySFX("GameOverWin");
        }
        else
        {
            GameManager.inst.AudioManager.PlaySFX("GameOverLose");
        }
    }
    public void SaveGame()
    {

    }
    public void LoadGame()
    {

    }
    public void QuitGame()
    {

    }
    private void OnDestroy()
    { 
        dealerController.OnCardClicked -= OnCardClicked;

        matchController.OnCardsMatch -= OnCardsMatch;
        matchController.OnCardsMissMatch -= OnCardsMissMatch;

        timerController.OnTimeEnds -= OnTimeEnds;
    }
}
