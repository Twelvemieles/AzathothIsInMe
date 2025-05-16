using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private DealerController dealerController;
    [SerializeField] private MatchController matchController;
    [SerializeField] private ScoreController scoreController;

    public void Start()
    {
        dealerController.OnCardClicked += OnCardClicked;
        dealerController.PopulateBoard(3, 4);
    }
    private void OnCardClicked(CardController card)
    {
        print(card.Data.cardType.ToString());
    }
    public void StartGamePlay()
    {

    }
    public void OnEndGame(bool win)
    {

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
    }
}
