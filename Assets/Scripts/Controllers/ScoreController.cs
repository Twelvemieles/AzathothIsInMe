using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ScoreData
{
    public int score;
    public int actualCombo;
    public int maxCombos;
    public int missMatches;
    public float time;
}
public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Transform scorePanel;

    private ScoreData _scoreData = new ScoreData();
    

    public ScoreData ScoreData => _scoreData;

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
    public void ResetCombos()
    {
        _scoreData.missMatches++;

        //This reduce the score everytime the player fails (could make the game harder)

/*        int scoreToReduce = -(int)((float)GameManager.inst.gameplayManager.GameDataConfig.gameplayData.matchPoints * GameManager.inst.gameplayManager.GameDataConfig.gameplayData.missMultiplierPoints);
        AddScore(scoreToReduce);*/

        _scoreData.actualCombo = 0;
        UpdateUI();
    }
    public void SetScoreTime(float time)
    {
        _scoreData.time = time;
    }
    public void ClearData()
    {
        _scoreData = new ScoreData();
        UpdateUI();
    }
    private void UpdateUI()
    {
        scoreText.text = _scoreData.score.ToString();
        comboText.text = _scoreData.actualCombo.ToString();
    }
    public void HideScorePanel()
    {
        scorePanel.gameObject.SetActive(false);
    }
    public  void ShowScorePanel(GameData gameData = null)
    {
        scorePanel.gameObject.SetActive(true);
        if(gameData != null)
        {
            _scoreData = gameData.scoreData;
        }
        UpdateUI();
    }
}
