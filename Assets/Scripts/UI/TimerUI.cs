using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class TimerUI : MonoBehaviour
{
    public event Action TimeEnd;

    [SerializeField]
    private TextMeshProUGUI _outputText;
    private string _format;


    [field: SerializeField]
    public float GameDurationSeconds { get; private set; }

    public float TimerSeconds { get; private set; }

    private bool _timerEnd;
    private void Start()
    {
    
       _format = _outputText.text;
        _timerEnd = false;
    }

    private void Update()
    {
        if (_timerEnd) return;

        TimerSeconds += Time.deltaTime;
        if (TimerSeconds >= GameDurationSeconds)
        {
            TimeEnd?.Invoke();
            _timerEnd = true;
        }
        int time = (int)(GameDurationSeconds - TimerSeconds);
        _outputText.text = String.Format(_format, time / 60, time % 60) ;
    }
}
