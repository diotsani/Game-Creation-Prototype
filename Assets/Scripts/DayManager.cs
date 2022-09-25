using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DayManager : MonoBehaviour
{
    public int amountDay;
    [SerializeField] private int maxDay;
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
        GetDay(amountDay);
    }
    void ChangeNextDay()
    {
        CheckDay();
        if(isMaxDay == false)
        {
            amountDay++;
            GetDay(amountDay);
        }
    }
    void CheckDay()
    {
        if (amountDay == maxDay)
        {
            isMaxDay = true;
            Debug.Log("Game Over");
        }
    }
    void SetDay(int SetDay)
    {
        switch (SetDay)
        {
            case 0:
                GetDay(SetDay);
                break;
            case 1:
                GetDay(SetDay);
                break;
            case 2:
                GetDay(SetDay);
                break;
            case 3:
                GetDay(SetDay);
                break;
            case 4:
                GetDay(SetDay);
                break;
        }
    }
    void GetDay(int GetDay)
    {
        Debug.Log("Day "+ GetDay);
    }
}
public enum ObjectState
{
    VeryGood,
    Good,
    LessGood,
    NotGood
}
