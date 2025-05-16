using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ScoreData
{
    public int score;
    public int actualCombo;
    public int maxCombos;
}
public class ScoreController : MonoBehaviour
{
    private ScoreData _scoreData = new ScoreData();

    public ScoreData ScoreData => _scoreData;
    public void AddScore(int deltaScore)
    {
        if(_scoreData.actualCombo > 0)
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
        if(_scoreData.actualCombo > _scoreData.maxCombos)
        {

            _scoreData.maxCombos = _scoreData.actualCombo;
        }
    }
    public void ResetCombos()
    {
        _scoreData.actualCombo = 0;
    }
    public void ClearData()
    {
        _scoreData = new ScoreData();
    }
}
