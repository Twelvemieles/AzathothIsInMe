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
public class CardController 
{
    private CardData _data;
    private CardView _view;
    private DealerController _dealerController;
    private bool _isCardAnimated;

    public bool IsCardAnimated => _isCardAnimated;

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

        PopulateCard();

        return this;
    }
    public CardController Init(CardData _cardData,CardView view, DealerController dealerController)
    {
        _view = view;
        _dealerController = dealerController;
        _data = new CardData()
        {
            cardState = CardData.CardState.FaceDown,
            cardType = _cardData.cardType,
            cardBoardIndex = _cardData.cardBoardIndex
        };

        PopulateCard();

        return this;
    }
    public void OnClick()
    {
        if(_data.cardState == CardData.CardState.FaceDown)
        { 
            _dealerController.OnClickedCard(this);
        }
    }

    public void ShowCard()
    {
        _isCardAnimated = true;
        _view.PlayShowCard();
        _data.cardState = CardData.CardState.FaceUp;
    }
    public void HideCard()
    {
        _isCardAnimated = true;
        _view.PlayHideCard();
        _data.cardState = CardData.CardState.FaceDown;
    }
    private void PopulateCard()
    {
        _view.SetCardImage(GameManager.inst.SpritesManager.GetCardSpriteWithType(_data.cardType));
    }
    public void OnFinishAnimation()
    {
        _isCardAnimated = false;
    }
    public void DestroyCard()
    {
        _view.DestroyCard();
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
    public CardController Init(CardData cardData, DealerController dealerController)
    {
        return _controller.Init(cardData ,this, dealerController);
    }
    public void ClickCard()
    {
        _controller.OnClick();
    }

    public void PlayShowCard()
    {
        animator.ResetTrigger("Hide");
        animator.SetTrigger("Show");
    }
    public void PlayHideCard()
    {
        animator.ResetTrigger("Show");
        animator.SetTrigger("Hide");
    }
    public void OnFinishAnimation()
    {
        _controller.OnFinishAnimation();
    }
    public void SetCardImage(Color sprite)
    {
        cardImage.color = sprite;
    }
    public void DestroyCard()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}


