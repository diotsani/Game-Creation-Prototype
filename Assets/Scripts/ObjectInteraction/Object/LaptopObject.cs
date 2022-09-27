using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaptopObject : BaseObject
{
    protected bool _isApplying;
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Laptop);
        base.Start();
        
        OnClickApplyJob();
    }
    void OnClickApplyJob()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(()=>OnApplyJobDecision(obj));
        }
    }
    void OnApplyJobDecision(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.ApplyJob)
        {
            _isApplying = true;
            obj.GetThisObject().SetActive(false);
            Debug.Log("You have applied for a job");
        }
        
    }
    protected override void OnCheckRepaired()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
           var obj = _decisionObjects[i];
           if (obj.decisionText == Constants.Requirments.Repair)
           {
               ResetAllDecision();
               ResetAmountClick();
               _objectState = ObjectState.Good;
               _isDamaged = false;
           }
        }
    }
    protected override void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        if(_isDamaged)return;
        for (int i = 0; i < decisionObjects.Count; i++)
        {
            var obj = decisionObjects[i];
            if (obj.decisionText == Constants.Requirments.ApplyJob)
            {
                obj.GetThisObject().SetActive(!_isApplying);
            }
            if (obj.decisionText == Constants.Requirments.TakeCourse)
            {
                bool set = playerStatusData.stress <= 50;
                obj.GetThisObject().SetActive(set);
                // if (playerStatusData.stress >= 50)
                // {
                //     obj.GetLockButton().SetActive(true);
                //     return;
                // }
                // obj.GetLockButton().SetActive(false);
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
            _decisionObjects[i].GetThisObject().SetActive(true);
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
            _objectState = ObjectState.Damaged;
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
            
            obj.GetThisObject().SetActive(false);
            if (obj.decisionText == Constants.Requirments.Repair)
            {
                obj.GetThisObject().SetActive(true);
            }
            //_decisionObjects[i].GetLockButton().gameObject.SetActive(true);
            //_decisionObjects[i].ServiceDecision(obj);
        }
    }
}
