using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
     
     public void StartGame()
    {
        GameManager.inst.StartGameplay(3, 4);
    }
}
