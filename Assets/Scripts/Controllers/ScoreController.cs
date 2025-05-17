using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
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
        
    }
    public void ResetCombos()
    {
        _scoreData.missMatches++;

        int scoreToReduce = -(int)((float)GameManager.inst.gameplayManager.GameDataConfig.gameplayData.matchPoints * GameManager.inst.gameplayManager.GameDataConfig.gameplayData.missMultiplierPoints);
        AddScore(scoreToReduce);
        _scoreData.actualCombo = 0;
    }
    public void SetScoreTime(float time)
    {
        _scoreData.time = time;
    }
    public void ClearData()
    {
        _scoreData = new ScoreData();
    }
}
