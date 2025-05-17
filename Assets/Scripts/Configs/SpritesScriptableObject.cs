using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpriteConfig
{
    public string id;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "SpriteConfig", menuName = "ScriptableObjects/SpritesScriptableObject", order = 1)]
public class SpritesScriptableObject : ScriptableObject
{
    public List<SpriteConfig> SpritesConfig;

}
