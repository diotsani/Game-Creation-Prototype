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
