using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchController : MonoBehaviour
{
    public event Action<CardController,CardController> OnCardsMatch;
    public event Action OnCardsMissMatch;
    public event Action OnCardsCombo;
     
    private CardController _cardA;
    private CardController _cardB;

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
    private bool DoCardsMatch()
    {
        return _cardA.Data.cardType == _cardB.Data.cardType;
    }
    IEnumerator DoMatchCards(CardController cardA,CardController cardB )
    {
        yield return new WaitUntil(() => !cardA.IsCardAnimated && !cardB.IsCardAnimated);

        OnCardsMatch?.Invoke(cardA, cardB);

    }
    IEnumerator DoMissMatchCards(CardController cardA, CardController cardB)
    {
        yield return new WaitUntil(() => !cardA.IsCardAnimated && !cardB.IsCardAnimated);

        cardA.HideCard();
        cardB.HideCard();

        OnCardsMissMatch?.Invoke();

    }

    public void ClearCheck()
    {
        _cardA = null;
        _cardB = null;
    }
}
