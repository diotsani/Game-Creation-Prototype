using UnityEngine;

public class HandphoneObject : BaseObject
{
    protected int consecutiveDay;
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Handphone);
        base.Start();
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
        Debug.Log("handphone is broken");
    }
}