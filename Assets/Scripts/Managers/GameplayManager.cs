using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
/// <summary>
/// Manages the core gameplay loop including card interactions, score tracking,
/// timer management, and handling win/loss states.
/// </summary>
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


    /// <summary>
    /// Initializes and starts a new gameplay session with the specified board size.
    /// </summary>
    /// <param name="horizontalCards">Number of cards per row</param>
    /// <param name="verticalCards">Number of cards per column</param>
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
    /// <summary>
    /// Called when two cards are matched. Updates score, combo, deletes cards, and checks win condition.
    /// </summary>
    /// <param name="cardA">First matched card</param>
    /// <param name="cardB">Second matched card</param>
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
    /// <summary>
    /// Checks whether all cards have been cleared from the board.
    /// If so, ends the game as a win.
    /// </summary>
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
    /// Ends the gameplay session, updates final score data, and triggers game over events.
    /// </summary>
    /// <param name="win">True if the player has won, false if lost</param>
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
    /// <summary>
    /// Saves the current game state, including card positions and score.
    /// </summary>
    public void SaveGame()
    {
        
        // Parse cardController List to CardData List 

        List<CardData> cardDatas = dealerController.Cards.Select(dto => new CardData
        {
            cardState = dto.Data.cardState,
        cardBoardIndex = dto.Data.cardBoardIndex,
        cardType = dto.Data.cardType
        }).ToList();

        // Updates Time on Score Data
        scoreController.SetScoreTime(timerController.ActualTime);

        // Creates the data to be saved
        GameData data = new GameData()
        {
            scoreData = scoreController.ScoreData,
            cards = cardDatas,
            cardsMatrix = dealerController.CardsMatrix
        };

        GameManager.inst.SaveGame(data);

    }
    /// <summary>
    /// Loads a previously saved game state and resumes the session.
    /// </summary>
    /// <param name="gameData">Saved game data to be restored</param>
    public void LoadGame(GameData gameData)
    {
        dealerController.PopulateBoard(gameData);
        timerController.Init(gameData.scoreData.time);
        scoreController.ShowScorePanel(gameData);
    }
    /// <summary>
    /// Triggers game exit logic and ends the current game session.
    /// </summary>
    public void QuitGame()
    {
        OnEndGame(false);
        GameManager.inst.QuitGameplay();
    } 
    private void OnDestroy()
    { 
        dealerController.OnCardClicked -= OnCardClicked;

        matchController.OnCardsMatch -= OnCardsMatch;
        matchController.OnCardsMissMatch -= OnCardsMissMatch;

        timerController.OnTimeEnds -= OnTimeEnds;
    }
}
