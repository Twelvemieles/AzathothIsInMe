using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using System;
/// <summary>
/// Controls the countdown timer during gameplay. It manages starting, stopping, updating the timer,
/// and invokes an event when the time reaches zero.
/// </summary>
public class TimerController : MonoBehaviour
{
    public event Action OnTimeEnds;

    [SerializeField] private TextMeshProUGUI timerText;
    private float _actualTime;
    private bool isTimeRunning;
    private Coroutine _timerCoroutine;

    public float ActualTime => _actualTime;
    /// <summary>
    /// Initializes the timer with a starting time value but doesn't start the countdown immediately.
    /// </summary>
    /// <param name="gameTime">Initial time to be set for the countdown (in seconds).</param>
    public void Init(float gameTime)
    {
        isTimeRunning = false;
        _actualTime = gameTime;
        OnStartTimer();
    }
    /// <summary>
    /// Starts the countdown timer if it is not already running.
    /// </summary>
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
    /// <summary>
    /// Stops the countdown timer and updates the UI with the current remaining time.
    /// </summary>
    public void OnStopTimer()
    {
        if (isTimeRunning)
        {
            isTimeRunning = false;
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);

                _timerCoroutine = null;
            }
            UpdateUI();
        }

    }

    /// <summary>
    /// Coroutine that runs every frame while the timer is active,
    /// decreasing the time and checking for timeout conditions.
    /// </summary>
    public IEnumerator OnTimerRun()
    {
        while (isTimeRunning)
        {
            _actualTime -= Time.deltaTime;
            if(_actualTime <= 0)
            {
                _actualTime = 0;
                OnStopTimer();
                OnTimeEnds?.Invoke();
            }
            UpdateUI();
            yield return null;
        }
    }
    /// <summary>
    /// Updates the UI text with the current remaining time formatted as MM:SS.
    /// </summary>
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
