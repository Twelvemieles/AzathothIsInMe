using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using System;
public class TimerController : MonoBehaviour
{
    public event Action OnTimeEnds;

    [SerializeField] private TextMeshProUGUI timerText;
    private float _actualTime;
    private bool isTimeRunning;
    private Coroutine _timerCoroutine;

    public float ActualTime => _actualTime;
    public void Init(float gameTime)
    {
        isTimeRunning = false;
        _actualTime = gameTime;
        OnStartTimer();
    }
    public void OnStartTimer()
    {
        if (!isTimeRunning)
        {
            isTimeRunning = true;
            if (gameObject.activeInHierarchy)
            {
                _timerCoroutine = StartCoroutine(OnTimerRun());
            }
             
        }
    }
    public void OnStopTimer()
    {
        if (isTimeRunning)
        {
            isTimeRunning = false;
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                OnTimeEnds?.Invoke();
                _timerCoroutine = null;
            }
            UpdateUI();
        }

    }

    public IEnumerator OnTimerRun()
    {
        while (isTimeRunning)
        {
            _actualTime -= Time.deltaTime;
            if(_actualTime <= 0)
            {
                _actualTime = 0;
                OnStopTimer();
            }
            UpdateUI();
            yield return null;
        }
    }
    private void UpdateUI()
    {
        TimeSpan t = TimeSpan.FromSeconds(_actualTime);
        string answer = "";
        answer = string.Format("{0:D2}:{1:D2}",
                            t.Seconds,
                            t.Milliseconds);

        timerText.text = answer;
    }

}
