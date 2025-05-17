using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// Populates the board with a random set of card pairs based on the specified dimensions.
    /// </summary>
    /// <param name="horizontalCards">Number of columns (X-axis)</param>
    /// <param name="verticalCards">Number of rows (Y-axis)</param>
    public void PopulateBoard(int horizontalCards, int verticalCards)
    {
        ClearCards();

        CreatePairsOfTypes((horizontalCards * verticalCards) / 2);

        cardMatrixPanel.GetComponent<GridLayoutGroup>().constraintCount = horizontalCards;

        StartCoroutine(DoEnableAndDisableCardsGroup());

        cardMatrixPanel.localScale = Vector3.one * GetMatrixScaleWithColumns(horizontalCards);

        _cardsMatrix = new Vector2(horizontalCards, verticalCards);


        StartCoroutine(ShowAllCards());
    }

    /// <summary>
    /// Populates the board using saved or predefined game data.
    /// </summary>
    /// <param name="gameData">Data container defining card matrix , card positions and scoreData.</param>
    public void PopulateBoard(GameData gameData)
    {
        ClearCards();

        int horizontalCards = (int)gameData.cardsMatrix.x;
        int verticalCards = (int)gameData.cardsMatrix.y;

        CreateAllCardsWithGameData(gameData);

        cardMatrixPanel.GetComponent<GridLayoutGroup>().constraintCount = horizontalCards;

        StartCoroutine(DoEnableAndDisableCardsGroup());

        cardMatrixPanel.localScale = Vector3.one * GetMatrixScaleWithColumns(horizontalCards);

        _cardsMatrix = new Vector2(horizontalCards, verticalCards);

    }
    private IEnumerator ShowAllCards()
    {
        foreach(var card in _cards)
        {
            card.ShowCard();
            yield return new WaitForSeconds(0.05f);
        }


        yield return new WaitForSeconds(1f);

        foreach (var card in _cards)
        {
            card.HideCard();
            yield return new WaitForSeconds(0.05f);
        }
    }
    /// <summary>
    /// Coroutine used to refresh the GridLayoutGroup and ensure correct layout alignment.
    /// </summary>
    private IEnumerator DoEnableAndDisableCardsGroup()
    {
        cardMatrixPanel.GetComponent<GridLayoutGroup>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        cardMatrixPanel.GetComponent<GridLayoutGroup>().enabled = false;
    }

    /// <summary>
    /// Calculates the scale factor for the board depending on how many columns it has.
    /// Larger grids are scaled down for better fit.
    /// </summary>
    /// <param name="input">Number of columns</param>
    /// <returns>Scale factor to apply to the board</returns>
    private float GetMatrixScaleWithColumns(float input)
    {
        if (input <= 2f) return 1f;
        if (input >= 5f) return 0.5f;

        // Interpolamos entre 1 y 0.5 en el rango [2, 5]
        float t = Mathf.InverseLerp(2f, 5f, input); // t va de 0 a 1
        return Mathf.Lerp(1f, 0.5f, t); // devuelve el valor interpolado
    }

    /// <summary>
    /// Generates a list of randomized pairs of card types based on the required pair count.
    /// </summary>
    /// <param name="totalCardTypes">Total unique card types needed (each will appear twice)</param>
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

    /// <summary>
    /// Instantiates and configures a full set of card instances from a list of types.
    /// </summary>
    /// <param name="cardTypes">List of card types (already randomized)</param>
    private void CreateAllCardsWithCardTypes(List<CardData.CardType> cardTypes)
    {
        foreach (var type in cardTypes)
        {
            CreateCard(type);
        }
    }
    /// <summary>
    /// Instantiates cards using the provided GameData, including empty spaces when the player loads the savved Data.
    /// </summary>
    private void CreateAllCardsWithGameData(GameData gameData)
    {
        int cardIndex = 0;
        for (int i = 0; i < gameData.cardsMatrix.x * gameData.cardsMatrix.y; i++)
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

    /// <summary>
    /// Instantiates a single card from a CardType (used when generating random pairs).
    /// </summary>
    private void CreateCard(CardData.CardType cardType)
    {
        var card = Instantiate(cardPrefab, cardMatrixPanel).Init(_cards.Count, cardType, this);

        _cards.Add(card);
    }
    /// <summary>
    /// Instantiates a card based on full CardData (used when restoring from GameData).
    /// </summary>
    private void CreateCard(CardData cardData)
    {
        var card = Instantiate(cardPrefab, cardMatrixPanel).Init(cardData, this);

        _cards.Add(card);
    }
    /// <summary>
    /// Instantiates an empty slot in the grid (no card).
    /// </summary>
    private void CreateEmpty()
    {
        Instantiate(emptyCardPrefab, cardMatrixPanel);
    }
    /// <summary>
    /// Invoked when a card is clicked, relays event to subscribers.
    /// </summary>
    public void OnClickedCard(CardController card)
    {
        OnCardClicked?.Invoke(card);
    }
    /// <summary>
    /// Removes and destroys a specific card from the board.
    /// </summary>
    public void DeleteCard(CardController card)
    {
        if (_cards.Contains(card))
        {
            _cards.Remove(card);
        }
        card.DestroyCard();
    }
    /// <summary>
    /// Clears all cards and placeholders from the board.
    /// </summary>
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
