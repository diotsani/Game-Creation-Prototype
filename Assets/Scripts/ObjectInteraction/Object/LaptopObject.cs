using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopObject : BaseObject
{
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Laptop);
        base.Start();
        
        OnClickRepair();
    }
    void OnClickRepair()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(()=>SetReapairDecision(obj));
        }
    }
    void SetReapairDecision(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.Repair)
        {
            ResetAllDecision();
            ResetAmountClick();
            _isDamaged = false;
            _objectState = ObjectState.Good;
        }
    }
    protected override void ObjectNeedRequirement(List<DecisionObject> objs)
    {
        if(_isDamaged)return;
        for (int i = 0; i < objs.Count; i++)
        {
            var obj = objs[i];
            
            if (obj.decisionText == Constants.Requirments.TakeCourse)
            {
                if (playerStatusData.stress >= 50)
                {
                    obj.GetLockButton().SetActive(true);
                    return;
                }
                obj.GetLockButton().SetActive(false);
            }
            if (obj.decisionText == Constants.Requirments.ApplyJob)
            {
                if (playerStatusData.skill >= 100)
                {
                    obj.GetLockButton().SetActive(false);
                    return;
                }
                obj.GetLockButton().SetActive(true);
            }
            if(obj.decisionText == Constants.Requirments.Repair)
            {
                obj.GetThisObject().SetActive(false);
            }
        }
    }

    public override void ResetAllDecision()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            _decisionObjects[i].GetLockButton().SetActive(false);
        }
    }
    protected override void CheckObjectState()
    {
        if(dayManager.amountDay == 1)
        {
            _objectState = ObjectState.Good;
        }
        if(dailyClicks >= 3)
        {
            Debug.Log("Laptop is damaged");
            _objectState = ObjectState.Damaged;
        }
        if(dayManager.amountDay >= 2 && dailyClicks >= 6)
        {
            //_objectState = ObjectState.Damaged;
        }
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
            _decisionObjects[i].GetLockButton().gameObject.SetActive(true);
            _decisionObjects[i].ServiceDecision(obj);
        }
    }
}
