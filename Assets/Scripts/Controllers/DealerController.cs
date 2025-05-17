using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DealerController : MonoBehaviour
{
    public event Action<CardController> OnCardClicked;

    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform emptyCardPrefab;
    [SerializeField] private Transform cardMatrixPanel;

    private Vector2 _cardsMatrix;

    private List<CardController> _cards = new List<CardController>();
    public List<CardController> Cards => _cards;

    public Vector2 CardsMatrix => _cardsMatrix;

    public void PopulateBoard(int horizontalCards, int verticalCards)
    {
        ClearCards();

        CreatePairsOfTypes((horizontalCards * verticalCards) / 2);

        cardMatrixPanel.GetComponent<GridLayoutGroup>().constraintCount = horizontalCards;

        StartCoroutine(DoEnableAndDisableCardsGroup());

        cardMatrixPanel.localScale = Vector3.one * GetMatrixScaleWithColumns(horizontalCards);

        _cardsMatrix = new Vector2 ( horizontalCards, verticalCards );
    }

    public void PopulateBoard(GameData gameData)
    {
        ClearCards();

        int horizontalCards = (int)gameData.cardsMatrix.x;
        int verticalCards = (int)gameData.cardsMatrix.y;

        CreateAllCardsWithGameData(gameData);

        cardMatrixPanel.GetComponent<GridLayoutGroup>().constraintCount = horizontalCards;

        StartCoroutine(DoEnableAndDisableCardsGroup());

        cardMatrixPanel.localScale = Vector3.one * GetMatrixScaleWithColumns(horizontalCards);

        _cardsMatrix = new Vector2 ( horizontalCards, verticalCards );
    }
    private IEnumerator DoEnableAndDisableCardsGroup()
    {
        cardMatrixPanel.GetComponent<GridLayoutGroup>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        cardMatrixPanel.GetComponent<GridLayoutGroup>().enabled = false;
    }
    private float GetMatrixScaleWithColumns(float input)
    {
        if (input <= 2f) return 1f;
        if (input >= 5f) return 0.5f;

        // Interpolamos entre 1 y 0.5 en el rango [2, 5]
        float t = Mathf.InverseLerp(2f, 5f, input); // t va de 0 a 1
        return Mathf.Lerp(1f, 0.5f, t); // devuelve el valor interpolado
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

        CreateAllCardsWithCardTypes(cardTypes);
    }
    private void CreateAllCardsWithCardTypes(List<CardData.CardType> cardTypes)
    {
        foreach(var type in cardTypes)
        {
            CreateCard(type);
        }
    }
    private void CreateAllCardsWithGameData(GameData gameData)
    {
        int cardIndex = 0;
        for(int i = 0; i <  gameData.cardsMatrix.x * gameData.cardsMatrix.y; i++)
        {
            if (i == gameData.cards[cardIndex].cardBoardIndex)
            { 
                CreateCard(gameData.cards[cardIndex]);
                cardIndex++;
            }
            else
            {
                CreateEmpty();
            } 
        }
    }
    private void CreateCard(CardData.CardType cardType)
    {
        var card = Instantiate(cardPrefab, cardMatrixPanel).Init(_cards.Count, cardType,this);
        
        _cards.Add(card);
    }
    private void CreateCard(CardData cardData)
    {
        var card = Instantiate(cardPrefab, cardMatrixPanel).Init( cardData, this);
        
        _cards.Add(card);
    }
    private void CreateEmpty( )
    {
        Instantiate(emptyCardPrefab, cardMatrixPanel);
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
        foreach(Transform transform in cardMatrixPanel)
        {
            Destroy(transform.gameObject);
        }
    }

}
