using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private List<BaseObject> _objects;

    private void OnEnable()
    {
        DecisionObject.OnClickInteracted += SetAllObjects;
    }

    private void OnDisable()
    {
        DecisionObject.OnClickInteracted -= SetAllObjects;
    }

    private void SetAllObjects()
    {
        foreach (var obj in _objects)
        {
            obj._isClicked = true;
        }
    }
}