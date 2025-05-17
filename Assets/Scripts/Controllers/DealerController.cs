using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DealerController : MonoBehaviour
{
    public event Action<CardController> OnCardClicked;

    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform cardMatrixPanel;

    private List<CardController> _cards = new List<CardController>();
    public List<CardController> Cards => _cards;

    public void PopulateBoard(int horizontalCards, int verticalCards)
    {
        ClearCards();
        CreatePairsOfTypes((horizontalCards * verticalCards) / 2);
        StartCoroutine(DoEnableAndDisableCardsGroup());
    }
    private IEnumerator DoEnableAndDisableCardsGroup()
    {
        cardMatrixPanel.GetComponent<GridLayoutGroup>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        cardMatrixPanel.GetComponent<GridLayoutGroup>().enabled = false;
    }
    private void CreatePairsOfTypes(int totalCardTypes)
    {
        List<CardData.CardType> cardTypes = new List<CardData.CardType>();
        for (int i = 0; i < totalCardTypes; i++)
        {
            cardTypes.Add((CardData.CardType)i);

            cardTypes.Add((CardData.CardType)i);
        }

        cardTypes = MatchDMTools.MixList(cardTypes);

        CreateAllCards(cardTypes);
    }
    private void CreateAllCards(List<CardData.CardType> cardTypes)
    {
        foreach(var type in cardTypes)
        {
            CreateCard(type);
        }
    }
    private void CreateCard(CardData.CardType cardType)
    {
        var card = Instantiate(cardPrefab, cardMatrixPanel).Init(_cards.Count, cardType,this);
        
        _cards.Add(card);
    }
    public void OnClickedCard(CardController card)
    {
        OnCardClicked?.Invoke(card);
    }
    public void DeleteCard(CardController card)
    {
        if(_cards.Contains(card))
        {
            _cards.Remove(card);
        }
        card.DestroyCard();
    }
    public void ClearCards()
    {
        foreach (var card in _cards)
        {
            card.DestroyCard();
        }
        _cards = new List<CardController>();
    }

}
