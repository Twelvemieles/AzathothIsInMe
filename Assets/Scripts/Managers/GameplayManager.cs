using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    public event Action<bool, ScoreData> OnEndGameAction;

    [SerializeField] private DealerController dealerController;
    [SerializeField] private MatchController matchController;
    [SerializeField] private ScoreController scoreController;

    public void Start()
    {
        dealerController.OnCardClicked += OnCardClicked;
        matchController.OnCardsMatch += OnCardsMatch;
        matchController.OnCardsMissMatch += OnCardsMissMatch;

    }


    public void StartGamePlay(int horizontalCards, int verticalCards)
    {

        dealerController.PopulateBoard(horizontalCards, verticalCards);
    }


    private void OnCardClicked(CardController card)
    {
        print(card.Data.cardType.ToString());
        matchController.CheckCard(card);
    }
    private void OnCardsMatch(CardController cardA, CardController cardB)
    {
        scoreController.AddScore(1);
        scoreController.AddCombo();
        dealerController.DeleteCard(cardA);
        dealerController.DeleteCard(cardB);

        CheckIfWin();

    }
    private void OnCardsMissMatch()
    {
        scoreController.ResetCombos();
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


    public void OnEndGame(bool win)
    {
        OnEndGameAction?.Invoke(win,scoreController.ScoreData);
        scoreController.ClearData();
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
    }
}
