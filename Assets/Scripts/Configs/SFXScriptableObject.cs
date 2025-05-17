using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SFXConfig
{
    public string id;
    public AudioClip audioClip;
}
[CreateAssetMenu(fileName = "SFXConfig", menuName = "ScriptableObjects/SFXConfigScriptableObject", order = 1)]
public class SFXScriptableObject : ScriptableObject
{
    public List<SFXConfig> SFXConfigs;

}
