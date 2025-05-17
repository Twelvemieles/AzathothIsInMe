using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Controls the logic of checking, matching, and mismatching cards in a memory game.
/// Handles state and triggers events based on the result of card comparisons.
/// </summary>
public class MatchController : MonoBehaviour
{
    public event Action<CardController,CardController> OnCardsMatch;
    public event Action OnCardsMissMatch;
    public event Action OnCardsCombo;
     
    private CardController _cardA;
    private CardController _cardB;
    /// <summary>
    /// Receives a card selection and determines whether it's a first or second pick.
    /// If both cards are selected, checks whether they match and starts the appropriate coroutine.
    /// </summary>
    public void CheckCard(CardController card)
    {
        if(_cardA == null)
        {
            _cardA = card;
            _cardA.ShowCard();
        }
        else if (_cardB == null && _cardA != card)
        {
            _cardB = card;
            _cardB.ShowCard();
        }

        if(_cardB != null && _cardA != null)
        {
            if (DoCardsMatch())
            {
                StartCoroutine(DoMatchCards(_cardA,_cardB));
            }
            else
            {
                StartCoroutine(DoMissMatchCards(_cardA, _cardB));
            }

            ClearCheck();
        }
    }
    /// <summary>
    /// Compares the type of the two selected cards.
    /// </summary>
    /// <returns>True if card types match, false otherwise.</returns>
    private bool DoCardsMatch()
    {
        return _cardA.Data.cardType == _cardB.Data.cardType;
    }
    /// <summary>
    /// Coroutine to handle logic when two cards match.
    /// Waits until both cards are idle (not animating), then triggers match event.
    /// </summary>
    IEnumerator DoMatchCards(CardController cardA,CardController cardB )
    {
        yield return new WaitUntil(() => !cardA.IsCardAnimated && !cardB.IsCardAnimated);

        OnCardsMatch?.Invoke(cardA, cardB);

    }
    /// <summary>
    /// Coroutine to handle logic when two cards do not match.
    /// Waits until both cards are idle (not animating), hides them, then triggers mismatch event.
    /// </summary>
    IEnumerator DoMissMatchCards(CardController cardA, CardController cardB)
    {
        yield return new WaitUntil(() => !cardA.IsCardAnimated && !cardB.IsCardAnimated);

        cardA.HideCard();
        cardB.HideCard();

        OnCardsMissMatch?.Invoke();

    }
    /// <summary>
    /// Resets the temporary selected cards, preparing for the next match check.
    /// </summary>
    public void ClearCheck()
    {
        _cardA = null;
        _cardB = null;
    }
}
