using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Serializable class representing the data of a card, including its type, state, and position on the board.
/// </summary>
[System.Serializable]
public class CardData 
{
    public CardType cardType;
    public CardState cardState;
    public int cardBoardIndex;
    public enum CardType
    {
        Monster1,
        Monster2,
        Monster3,
        Monster4,
        Monster5,
        Monster6,
        Monster7,
        Monster8,
        Monster9,
        Monster10,
        Monster11,
        Monster12,
        Monster13,
        Monster14,
        Monster15
    }
    public enum CardState
    {
        FaceUp,
        FaceDown,
        Hidden
    }

}
/// <summary>
/// Core controller class that manages the logic and behavior of a card.
/// It handles card state changes, interaction logic, and view updates.
/// </summary>
public class CardController 
{
    private CardData _data;
    private CardView _view;
    private DealerController _dealerController;
    private bool _isCardAnimated;

    public bool IsCardAnimated => _isCardAnimated;

    public CardData Data => _data;
    /// <summary>
    /// Initializes the card with specific type and position, sets default state to FaceDown.
    /// </summary>
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
    /// <summary>
    /// Initializes the card using existing CardData, overriding its state to FaceDown.
    /// </summary>
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
    /// <summary>
    /// Handles click logic. Only reacts if the card is currently face down.
    /// </summary>
    public void OnClick()
    {
        if(_data.cardState == CardData.CardState.FaceDown)
        { 
            _dealerController.OnClickedCard(this);
        }
    }

    /// <summary>
    /// Flips the card face up and plays the show animation.
    /// </summary>
    public void ShowCard()
    {
        _isCardAnimated = true;
        _view.PlayShowCard();
        _data.cardState = CardData.CardState.FaceUp;
    }
    /// <summary>
    /// Flips the card face down and plays the hide animation.
    /// </summary>
    public void HideCard()
    {
        _isCardAnimated = true;
        _view.PlayHideCard();
        _data.cardState = CardData.CardState.FaceDown;
    }
    /// <summary>
    /// Sets the visual representation of the card based on its type.
    /// </summary>
    private void PopulateCard()
    {
        _view.SetCardImage(GameManager.inst.SpritesManager.GetCardSpriteWithType(_data.cardType));
    }

    /// <summary>
    /// Called at the end of the card's animation to clear the animation flag.
    /// </summary>
    public void OnFinishAnimation()
    {
        _isCardAnimated = false;
    }


    public void DestroyCard()
    {
        _view.DestroyCard();
    }
}

/// <summary>
/// View component of the card. Manages visuals, animations, and click interactions.
/// </summary>
public class CardView : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer cardImage;

    private CardController _controller = new CardController();
    /// <summary>
    /// Initializes the view and controller with new card data.
    /// </summary>
    public CardController Init(int cardBoardIndex, CardData.CardType cardType, DealerController dealerController)
    {
        return _controller.Init(cardType,this,cardBoardIndex, dealerController);
    }

    /// <summary>
    /// Initializes the view and controller with existing card data.
    /// </summary>
    public CardController Init(CardData cardData, DealerController dealerController)
    {
        return _controller.Init(cardData ,this, dealerController);
    }
    public void ClickCard()
    {
        _controller.OnClick();
    }
    /// <summary>
    /// Triggers the show (flip face-up) animation on the card.
    /// </summary>
    public void PlayShowCard()
    {
        animator.ResetTrigger("Hide");
        animator.SetTrigger("Show");
    }

    /// <summary>
    /// Triggers the hide (flip face-down) animation on the card.
    /// </summary>
    public void PlayHideCard()
    {
        animator.ResetTrigger("Show");
        animator.SetTrigger("Hide");
    }
    /// <summary>
    /// Notifies the controller when the animation has completed, this is being called with animation event.
    /// </summary>
    public void OnFinishAnimation()
    {
        _controller.OnFinishAnimation();
    }
    /// <summary>
    /// Applies a sprite to the card image based on the assigned type.
    /// </summary>
    public void SetCardImage(Sprite sprite)
    {
        cardImage.sprite = sprite;
    }
    /// <summary>
    /// Destroys the card GameObject from the scene.
    /// </summary>
    public void DestroyCard()
    {
        MonoBehaviour.Destroy(gameObject);
    }
}


