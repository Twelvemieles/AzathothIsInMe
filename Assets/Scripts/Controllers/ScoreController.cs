using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Serializable class that holds score-related data for the game session.
/// </summary>
[System.Serializable]
public class ScoreData
{
    public int score;
    public int actualCombo;
    public int maxCombos;
    public int missMatches;
    public float time;
}
/// <summary>
/// Controls all score-related behavior including combo tracking, UI updates, and score panel visibility.
/// </summary>
public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Transform scorePanel;

    private ScoreData _scoreData = new ScoreData();
    

    public ScoreData ScoreData => _scoreData;

    /// <summary>
    /// Adds a given score value, optionally multiplied by the current combo streak.
    /// Ensures score never drops below zero.
    /// </summary>
    /// <param name="deltaScore">Base score to add.</param>
    public void AddScore(int deltaScore)
    {
        if(_scoreData.actualCombo > 1)
        {

            _scoreData.score += deltaScore * _scoreData.actualCombo;
        }
        else
        {

            _scoreData.score += deltaScore;
        }

        if(_scoreData.score <= 0)
        {
            _scoreData.score = 0;
        }

        UpdateUI();
    }
    /// <summary>
    /// Increments combo streak and updates maximum combo if necessary.
    /// Plays different sound effects based on combo count.
    /// </summary>
    public void AddCombo()
    {
        _scoreData.actualCombo++;
        if(_scoreData.actualCombo > _scoreData.maxCombos && _scoreData.actualCombo > 1)
        {

            _scoreData.maxCombos = _scoreData.actualCombo - 1;
            switch(_scoreData.actualCombo)
            {
                case 2:

                    GameManager.inst.AudioManager.PlaySFX("Combo");
                    break;
                case 3:

                    GameManager.inst.AudioManager.PlaySFX("DoubleCombo");
                    break;
                 default:

                    GameManager.inst.AudioManager.PlaySFX("TripleCombo");
                    break;
            }
        }
        UpdateUI();


    }
    /// <summary>
    /// Resets the current combo streak and increments miss count.
    /// Optionally could reduce score (logic commented out).
    /// </summary>
    public void ResetCombos()
    {
        _scoreData.missMatches++;

        //This reduce the score everytime the player fails (could make the game harder)

/*        int scoreToReduce = -(int)((float)GameManager.inst.gameplayManager.GameDataConfig.gameplayData.matchPoints * GameManager.inst.gameplayManager.GameDataConfig.gameplayData.missMultiplierPoints);
        AddScore(scoreToReduce);*/

        _scoreData.actualCombo = 0;
        UpdateUI();
    }
    /// <summary>
    /// Sets the current time value in the score data (can be used for tracking elapsed/remaining time).
    /// </summary>
    /// <param name="time">Time value to store.</param>
    public void SetScoreTime(float time)
    {
        _scoreData.time = time;
    }
    /// <summary>
    /// Resets all score-related data and updates UI accordingly.
    /// </summary>
    public void ClearData()
    {
        _scoreData = new ScoreData();
        UpdateUI();
    }
    /// <summary>
    /// Updates the score and combo UI text fields to reflect current values.
    /// </summary>
    private void UpdateUI()
    {
        scoreText.text = _scoreData.score.ToString();
        comboText.text = _scoreData.actualCombo.ToString();
    }
    /// <summary>
    /// Hides the entire score panel (used for transitions or scene management).
    /// </summary>
    public void HideScorePanel()
    {
        scorePanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// Shows the score panel and optionally loads existing score data from a saved game session.
    /// </summary>
    /// <param name="gameData">Optional game data to restore the score state from.</param>
    public void ShowScorePanel(GameData gameData = null)
    {
        scorePanel.gameObject.SetActive(true);
        if(gameData != null)
        {
            _scoreData = gameData.scoreData;
        }
        UpdateUI();
    }
}
