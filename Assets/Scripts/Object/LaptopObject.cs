using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopObject : BaseObject
{
    public override void Start()
    {
        decisionScriptable = Resources.Load<DecisionScriptable>(Constants.GetData.Laptop);
        base.Start();
    }

    public override void OnCheckObject()
    {
        base.OnCheckObject();
        if (objectState == ObjectState.LessGood)
        {
            
        }
    }
}
