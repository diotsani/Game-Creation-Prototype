using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : BaseObject
{
    protected override void Start()
    {
        _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Door);
        base.Start();
    }

    protected override void ObjectNeedRequirement(List<DecisionObject> decisionObjects)
    {
        for (int i = 0; i < decisionObjects.Count; i++)
        {
            var obj = decisionObjects[i];
            
            if (obj.decisionText == Constants.Requirments.BuyFood)
            {
                bool set = playerStatusData.money >= 20;
                obj.GetThisObject().SetActive(set);
            }
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
        //throw new System.NotImplementedException();
    }
}
