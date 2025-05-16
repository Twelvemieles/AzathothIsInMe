using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
   [HideInInspector] public SpritesManager SpritesManager;
   [HideInInspector] public UIManager UIManager;
   [HideInInspector] public AudioManager AudioManager;
   [HideInInspector] public SaveManager SaveManager;


    private void Start()
    {
        if(inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SpritesManager = GetComponent<SpritesManager>();
    }
}
