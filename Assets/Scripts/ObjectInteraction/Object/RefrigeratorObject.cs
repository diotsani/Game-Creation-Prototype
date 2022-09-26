using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorObject : BaseObject
{
    protected int consecutiveDay;
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Refrigerator);
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
            playerStatusData.food = 0;
            obj.GetThisObject().SetActive(false);
        }
    }

    public override void ResetAllDecision()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDamagedObject()
    {
        throw new System.NotImplementedException();
    }

    protected override void CheckObjectState()
    {
        if (dailyClicks == 0)
        {
            consecutiveDay++;
        }
        
        if(consecutiveDay == 3)
        {
            Debug.Log("food is stale");
        }
    }
}
