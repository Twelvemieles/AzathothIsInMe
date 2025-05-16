using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchController : MonoBehaviour
{
    public event Action<CardController,CardController> OnCardsMatch;
    public event Action OnCardsMissMatch;
    public event Action OnCardsCombo;

    private int _comboCounter;
    private CardController _cardA;
    private CardController _cardB;

    public void CheckCard(CardController card)
    {
        if(_cardA == null)
        {
            _cardA = card;
        }
        else if (_cardB == null && _cardA != card)
        {
            _cardB = card;
        }

        if(_cardB != null && _cardA != null)
        {
            if (DoCardsMatch())
            {
                OnMatch();
            }
            else
            {
                OnMissMatch();
            }

            ClearCheck();
        }
    }
    private bool DoCardsMatch()
    {
        return _cardA.Data.cardType == _cardB.Data.cardType;
    }
    private void OnMatch()
    {
        OnCardsMatch?.Invoke(_cardA, _cardB);
    }
    private void OnMissMatch()
    {
        OnCardsMissMatch?.Invoke();
    }
    private void OnCombo()
    {
        OnCardsCombo?.Invoke();
    }


    private void ClearCheck()
    {
        _cardA = null;
        _cardB = null;
    }
}
