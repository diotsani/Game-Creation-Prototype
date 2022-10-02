using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DayManager : MonoBehaviour
{
    public delegate void EventName();
    public static event EventName OnGameover;
    public int amountDay { get; private set; }
    private int maxDay;
    private bool isMaxDay;

    private void OnEnable()
    {
        GameFlow.OnNextDay += ChangeNextDay;
    }
    private void OnDisable()
    {
        GameFlow.OnNextDay -= ChangeNextDay;
    }
    void Start()
    {
        amountDay = 1;
        maxDay = 7;
    }
    void ChangeNextDay()
    {
        CheckDay();
        if(isMaxDay == false)
        {
            amountDay++;
        }
    }
    void CheckDay()
    {
        if (amountDay == maxDay)
        {
            isMaxDay = true;
            OnGameover?.Invoke();
            Debug.Log("Game Over");
        }
    }
}
