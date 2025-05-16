using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{

    [SerializeField] private CardType cardType;
    [SerializeField] private CardState cardState;
    enum CardType
    {
        Red,
        Blue,
        White,
        Black,
        Green,
        Yellow,
        Purple
    }
    enum CardState
    {
        FaceUp,
        FaceDown,
        Hidden
    }

}
public class CardView : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Image cardImage;

}
public class CardController : MonoBehaviour
{
    private CardData cardData;
    private CardView cardView;

    private void Start()
    {
        cardData = GetComponent<CardData>();
        cardView = GetComponent<CardView>();
    }
}
