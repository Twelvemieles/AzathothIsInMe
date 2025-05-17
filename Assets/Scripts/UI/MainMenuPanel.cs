using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
     
     public void StartGameEasy()
    {
        GameManager.inst.StartGameplay(2, 2);
    }
    public void StartGameMedium()
    {
        GameManager.inst.StartGameplay(2, 3);
    }
    public void StartGameHard()
    {
        GameManager.inst.StartGameplay(5, 6);
    }
}
