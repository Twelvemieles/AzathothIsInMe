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
   public GameplayManager gameplayManager;


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
        UIManager = GetComponent<UIManager>();
        AudioManager = GetComponent<AudioManager>();
        SaveManager = GetComponent<SaveManager>(); 

        gameplayManager.OnEndGameAction += EndGamePlay;


        GameManager.inst.AudioManager.PlayMusic("BackgroundMusic");
    }

    public void StartGame()
    {

    }

    public void StartGameplay(int horizontalCards, int verticalCards)
    {
        UIManager.ShowGameplayScreen();
        gameplayManager.StartGamePlay(horizontalCards, verticalCards);
    }
    public void EndGamePlay(bool win,ScoreData scoreData)
    {
        UIManager.OnEndgame(win, scoreData);
    }
    public void SaveGame(GameData data)
    {
        SaveManager.Save(data);
    }
    public void LoadGame ()
    {
        var gameData = SaveManager.Load();
        if(gameData != null)
        {

            UIManager.ShowGameplayScreen();

            gameplayManager.LoadGame(gameData);
        }
    }
    public void QuitGameplay()
    {

    }

    public void QuitGame()
    {

    }

}
