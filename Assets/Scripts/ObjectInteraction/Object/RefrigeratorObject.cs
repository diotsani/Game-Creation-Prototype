using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorObject : BaseObject
{
    //protected int consecutiveDay;
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Refrigerator);
        base.Start();
        OnClickThrow();
    }
    void OnClickThrow()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(() => OnThrowFood(obj));
        }
    }
    void OnThrowFood(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.ThrowFood)
        {
            ResetAllDecision();
            ResetConsecutiveDay();
            playerStatusData.ResetFood();
            _objectState = ObjectState.Good;
            _isDamaged = false;
        }
    }

    protected override void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        if(_isDamaged)return;
        for (int i = 0; i < decisionObjects.Count; i++)
        {
            var obj = decisionObjects[i];
            if (obj.decisionText == Constants.Requirments.ThrowFood)
            {
                obj.GetThisObject().SetActive(false);
            }
            if (obj.decisionText == Constants.Requirments.Eat)
            {
                bool set = playerStatusData.food >= 10;
                obj.GetThisObject().SetActive(set);
                // if (playerStatusData.food >= 10)
                // {
                //     obj.GetThisObject().SetActive(true);
                // }
                // else
                // {
                //     obj.GetThisObject().SetActive(false);
                // }
            }
        }
    }

    public override void ResetAllDecision()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            _decisionObjects[i].GetThisObject().SetActive(true);
        }
    }
    protected override void CheckObjectState()
    {
        if (dailyClicks != 0) return;
        consecutiveDay++;
        
        if(consecutiveDay != 3) return;
        _objectState = ObjectState.Damaged;
        Debug.Log("food is stale");
        
        OnChangeState();
    }
    private void OnChangeState()
    {
        switch (_objectState)
        {
            case ObjectState.Good:
                break;
            case ObjectState.Damaged:
                OnDamagedObject();
                break;
        }
    }
    protected override void OnDamagedObject()
    {
        _isDamaged = true;
        
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.GetThisObject().SetActive(false);

            if (obj.decisionText == Constants.Requirments.ThrowFood)
            {
                obj.GetThisObject().SetActive(true);
            }
        }
    }
}
