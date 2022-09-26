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

    [Header("Config")]
    private int _maxInteractables = 3;
    [SerializeField] private int _amountInteractables;
    [SerializeField] private int _resetInteractables;

    private PlayerStatusData playerStatusData;
    private void OnEnable()
    {
        DecisionObject.OnClickInteracted += ReduceInteractable;
    }

    private void OnDisable()
    {
        DecisionObject.OnClickInteracted -= ReduceInteractable;
    }

    private void Start()
    {
        playerStatusData = PlayerStatusData.instance;
        
        _amountInteractables = _maxInteractables;
        _resetInteractables = 0;
    }

    private void Update()
    {
        ResetInteractables();
    }
    void ReduceInteractable()
    {
        _amountInteractables--;
    }
    void ResetInteractables()
    {
        if (_amountInteractables == _resetInteractables)
        {
            OnNewDay();
            _amountInteractables = _maxInteractables;
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
