using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private List<BaseObject> _objects;

    private void OnEnable()
    {
        DecisionObject.OnClickInteracted += SetAllObjects;
        DayManager.OnGameover += OnGameOver;
    }

    private void OnDisable()
    {
        DecisionObject.OnClickInteracted -= SetAllObjects;
        DayManager.OnGameover -= OnGameOver;
    }

    private void SetAllObjects()
    {
        foreach (var obj in _objects)
        {
            obj._isClicked = true;
        }
    }
    private void OnGameOver()
    {
        foreach (var obj in _objects)
        {
            obj.DeactivateDecisionButton();
        }
    }
}