using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{

    [SerializeField] private List<Color> cardSprites;
    public Color GetCardSpriteWithType(CardData.CardType type)
    {
        return cardSprites[(int)type];
    }
}
