using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameplayData
{
    public int matchPoints;
    public float gameTimeSeconds;

}
[CreateAssetMenu(fileName = "GameplayDataConfig", menuName = "ScriptableObjects/GameplayDataScriptableObject", order = 1)]
public class GameplayDataScriptableObject : ScriptableObject
{
    public GameplayData gameplayData;

}

