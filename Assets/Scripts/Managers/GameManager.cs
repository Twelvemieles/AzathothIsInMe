using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The GameManager is the central controller for high-level game logic.
/// It manages key game systems, initializes dependencies, and provides global access to game-wide operations.
/// </summary>
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

        StartGame();
    }
    /// <summary>
    /// Initializes game systems and binds relevant events.
    /// Also starts background music playback.
    /// </summary>
    public void StartGame()
    {
        SpritesManager = GetComponent<SpritesManager>();
        UIManager = GetComponent<UIManager>();
        AudioManager = GetComponent<AudioManager>();
        SaveManager = GetComponent<SaveManager>();

        gameplayManager.OnEndGameAction += EndGamePlay;


        GameManager.inst.AudioManager.PlayMusic("BackgroundMusic");
    }
    /// <summary>
    /// Starts a new gameplay session by configuring the board and showing the UI.
    /// </summary>
    /// <param name="horizontalCards">Number of cards horizontally on the board</param>
    /// <param name="verticalCards">Number of cards vertically on the board</param>
    public void StartGameplay(int horizontalCards, int verticalCards)
    {
        UIManager.ShowGameplayScreen();
        gameplayManager.StartGamePlay(horizontalCards, verticalCards);
    }
    /// <summary>
    /// Ends the current gameplay session and updates the UI with results.
    /// </summary>
    /// <param name="win">Indicates if the player has won</param>
    /// <param name="scoreData">Final score data of the session</param>
    public void EndGamePlay(bool win,ScoreData scoreData)
    {
        UIManager.OnEndgame(win, scoreData);
    }
    /// <summary>
    /// Saves the current game state using the save manager.
    /// </summary>
    /// <param name="data">Data representing the current game state</param>
    public void SaveGame(GameData data)
    {
        SaveManager.Save(data);
    }
    /// <summary>
    /// Loads a previously saved game and resumes gameplay.
    /// </summary>
    public void LoadGame ()
    {
        var gameData = SaveManager.Load();
        if(gameData != null)
        {

            UIManager.ShowGameplayScreen();

            gameplayManager.LoadGame(gameData);
        }
    }

    /// <summary>
    /// Forces the end of gameplay, marking it as a loss.
    /// </summary>
    public void QuitGameplay()
    {
        EndGamePlay(false,null);
    }
    /// <summary>
    /// Exits the application entirely.
    /// </summary>
    public void QuitGameApp()
    {
        Application.Quit();
    }

}
