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
    private DealerController _dealerController;

    public CardData Data => _data;

    public CardController Init(CardData.CardType _cardType,CardView view, int _cardBoardIndex, DealerController dealerController)
    {
        _view = view;
        _dealerController = dealerController;
        _data = new CardData()
        {
            cardState = CardData.CardState.FaceDown,
            cardType = _cardType,
            cardBoardIndex = _cardBoardIndex
        };

        _view.SetCardImage(GameManager.inst.SpritesManager.GetCardSpriteWithType(_data.cardType));

        return this;
    }
    public void OnClick()
    {
        _dealerController.OnClickedCard(this);
    }
}
public class CardView : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer cardImage;

    private CardController _controller = new CardController();

    public CardController Init(int cardBoardIndex, CardData.CardType cardType, DealerController dealerController)
    {
        return _controller.Init(cardType,this,cardBoardIndex, dealerController);
    }
    public void ClickCard()
    {
        _controller.OnClick();
    }
    public void SetCardImage(Color sprite)
    {
        cardImage.color = sprite;
    }

}


