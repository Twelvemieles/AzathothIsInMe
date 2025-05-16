using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardData 
{
    public CardType cardType;
    public CardState cardState;
    public int cardBoardIndex;
    public enum CardType
    {
        Red,
        Blue,
        White,
        Black,
        Green,
        Yellow,
        Purple
    }
    public enum CardState
    {
        FaceUp,
        FaceDown,
        Hidden
    }

}
public class CardController : MonoBehaviour
{
    private CardData _data;
    private CardView _view;

    public CardController Init(CardData.CardType _cardType,CardView view, int _cardBoardIndex)
    {
        _view = view;
        _data = new CardData()
        {
            cardState = CardData.CardState.FaceDown,
            cardType = _cardType,
            cardBoardIndex = _cardBoardIndex
        };

        return this;
    }
}
public class CardView : MonoBehaviour
{
    [SerializeField] private CardData.CardType cardType;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer cardImage;

    private CardController _controller = new CardController();

    private CardController Init(int cardBoardIndex)
    {
        return _controller.Init(cardType,this,cardBoardIndex);
    }

}


