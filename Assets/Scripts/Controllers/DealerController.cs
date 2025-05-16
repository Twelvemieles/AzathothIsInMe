using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DealerController : MonoBehaviour
{
    public event Action<CardController> OnCardClicked;

    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform cardMatrixPanel;

    private List<CardController> cards = new List<CardController>();

    public void PopulateBoard(int horizontalCards, int verticalCards)
    {
        ClearCards();
        CreatePairsOfTypes((horizontalCards * verticalCards) / 2);
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
        var card = Instantiate(cardPrefab, cardMatrixPanel).Init(cards.Count, cardType,this);
        cards.Add(card);
    }
    public void OnClickedCard(CardController card)
    {
        OnCardClicked?.Invoke(card);
    }
    public void DeleteCard(CardController card)
    {

    }
    public void ClearCards()
    {
        foreach(var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards = new List<CardController>();
    }

}
