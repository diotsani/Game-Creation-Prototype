using System.Collections.Generic;
using UnityEngine;

public class HandphoneObject : BaseObject
{
    //protected int consecutiveDay;
    protected bool _isSale;
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Handphone);
        base.Start();
        OnClickRepair();
        
    }
    void OnClickRepair()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(() => OnRepairObject(obj));
        }
    }
    void OnRepairObject(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.SocialMedia)
        {
            gameFlow.AddInteractable();
        }
        if(obj.decisionText == Constants.Requirments.Sell)
        {
            _isSale = true;
            GetDecisionParent(!_isSale);
            obj.GetThisObject().SetActive(!_isSale);
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
                ResetConsecutiveDay();
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
            if (obj.decisionText == Constants.Requirments.Sell)
            {
                obj.GetThisObject().SetActive(!_isSale);
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
        if (dailyClicks != 3)
        {
            consecutiveDay = 0;
            return;
        }
        consecutiveDay++;
        if(consecutiveDay != 2)
        {
            return;
        }
        _objectState = ObjectState.Damaged;
        Debug.Log("handphone is broken");
        
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
        }
    }
}