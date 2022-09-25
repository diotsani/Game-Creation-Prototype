using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public delegate void EventName();
    public static event EventName OnNextDay;
    public static event EventName OnChangeState;
    public static event EventName OnResetClick;
    
    [SerializeField] private int amountInteractables;
    [SerializeField] private int maxInteractables;

    private PlayerStatusData playerStatusData;
    private void OnEnable()
    {
        DecisionObject.OnClickInteracted += AddInteractable;
    }

    private void OnDisable()
    {
        DecisionObject.OnClickInteracted -= AddInteractable;
    }

    private void Start()
    {
        playerStatusData = PlayerStatusData.instance;
        amountInteractables = 0;
        maxInteractables = 3;
    }

    private void Update()
    {
        ResetInteractables();
    }
    void AddInteractable()
    {
        amountInteractables++;
    }
    void ResetInteractables()
    {
        if (amountInteractables == maxInteractables)
        {
            OnNewDay();
            amountInteractables = 0;
        }
    }

    private void OnNewDay()
    {
        playerStatusData.ReduceHealth();
        
        OnNextDay?.Invoke();
        OnChangeState?.Invoke();
        OnResetClick?.Invoke();
    }
}
