using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{

    [SerializeField] private SpritesScriptableObject spritesConfig;
    public Sprite GetCardSpriteWithType(CardData.CardType type)
    {
        return spritesConfig.SpritesConfig[(int)type].sprite;
    }
}
