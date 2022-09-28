using System.Collections.Generic;
using UnityEngine;

public class HandphoneObject : BaseObject
{
    //protected int consecutiveDay;
    protected bool _isSale;

    private bool isClickThis;
    [SerializeField]protected int saveClick;
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Handphone);
        base.Start();
        OnClickSpecificDecision();
        
    }
    protected override void Update()
    {
        if(_isClicked)
        {
            ObjectNeedRequirement(_decisionObjects);
            CheckCondition();
            _isClicked = false;
        }
    }
    void OnClickSpecificDecision()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            obj.button.onClick.AddListener(() => SpecificDecision(obj));
        }
    }
    void SpecificDecision(DecisionObject obj)
    {
        if (obj.decisionText == Constants.Requirments.SocialMedia)
        {
            Debug.Log("Job Info");
            isClickThis = true;
            gameFlow.AddInteractable();
        }
        
        if(obj.decisionText == Constants.Requirments.Sell)
        {
            _isSale = true;
            GetDecisionParent(!_isSale);
            obj.GetThisObject().SetActive(!_isSale);
        }
        
        if (obj.decisionText == Constants.Requirments.Repair)
        {
            ResetAllDecision();
            ResetConsecutiveDay();
            ResetAmountClick();
            ResetDailyClicks();
            saveClick = 0;
            
            _objectState = ObjectState.Good;
            _isDamaged = false;
        }
    }
    protected override void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        SetRepairDecision();
        if(_isDamaged)return;
        for (int i = 0; i < decisionObjects.Count; i++)
        {
            var obj = decisionObjects[i];
            if (obj.decisionText == Constants.Requirments.Sell)
            {
                obj.GetThisObject().SetActive(!_isSale);
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
    protected void CheckCondition()
    {
        if (dailyClicks >= 1)
        {
            if (isClickThis)
            {
                saveClick++;
                isClickThis = false;
            }
        }
        if(consecutiveDay >=1 && saveClick >= 6)
        {
            _objectState = ObjectState.Damaged;
            OnChangeState();
        }
        if (dailyClicks != 6)return;
        _objectState = ObjectState.Damaged;
        OnChangeState();
    }
    
    protected override void CheckObjectState()
    {
        if (saveClick >= 1)
        {
            consecutiveDay++;
        }

        if (dailyClicks <= 0)
        {
            consecutiveDay = 0;
            saveClick = 0;
        }

        if (consecutiveDay >= 2 && saveClick == 6)
        {
            _objectState = ObjectState.Damaged;
            Debug.Log("handphone is broken");
        
            OnChangeState();
        }
        else if(consecutiveDay >= 2 && saveClick <= 6)
        {
            consecutiveDay = 0;
            saveClick = 0;
        }
        // if (dailyClicks != 3)
        // {
        //     consecutiveDay = 0;
        //     return;
        // }
        // consecutiveDay++;
        // if(consecutiveDay != 2)
        // {
        //     return;
        // }
        
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
    void SetRepairDecision()
    {
        for (int i = 0; i < _decisionObjects.Count; i++)
        {
            var obj = _decisionObjects[i];
            if(obj.decisionText == Constants.Requirments.Repair)
            {
                bool set = playerStatusData.money >= 25;
                obj.GetLockButton().SetActive(!set);
                obj.SetLockButton();
                
                obj.GetThisObject().SetActive(_isDamaged);
            }
        }
    }
}